using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace HypnoControlLauncher {
  static class Program {
    const string Api = "https://api.github.com/repos/heretoholdbeer/HypnoControl/releases?per_page=100";
    const string Manifest = "https://raw.githubusercontent.com/heretoholdbeer/HypnoControl-Downloads/main/downloads/current.json";
    static readonly string Root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"HypnoControl","UpdateManager");
    static readonly string Versions = Path.Combine(Root,"Versions");
    static readonly string CurrentFile = Path.Combine(Root,"current.txt");
    static JavaScriptSerializer Json = new JavaScriptSerializer();
    class Release { public string Tag, Url, Sha; public long Size; public Version Number; }
    static Dictionary<string,object> Obj(object x) { return x as Dictionary<string,object>; }
    static string Str(Dictionary<string,object> d,string k) { return d != null && d.ContainsKey(k) && d[k]!=null ? Convert.ToString(d[k]) : ""; }
    static long Long(Dictionary<string,object> d,string k) { long n; return long.TryParse(Str(d,k),out n)?n:0; }
    static Version Number(string tag) {
      var m=Regex.Match(tag??"",@"^v(\d+)\.(\d+)\.(\d+)-alpha\.(\d+)$",RegexOptions.IgnoreCase);
      return m.Success?new Version(int.Parse(m.Groups[1].Value),int.Parse(m.Groups[2].Value),int.Parse(m.Groups[3].Value),int.Parse(m.Groups[4].Value)):null;
    }
    static WebClient Client() {
      var w=new WebClient();
      w.Headers[HttpRequestHeader.UserAgent]="HypnoControl-Launcher/2.0";
      w.Headers[HttpRequestHeader.Accept]="application/vnd.github+json";
      w.Encoding=System.Text.Encoding.UTF8;
      return w;
    }
    static Release FromManifest() {
      using(var w=Client()) {
        var d=Obj(Json.DeserializeObject(w.DownloadString(Manifest)));
        var tag=Str(d,"tag");
        var n=Number(tag);
        if(n==null) throw new Exception("Versionsindex enthaelt keine gueltige Alpha-Version.");
        if(!Str(d,"url").StartsWith("https://raw.githubusercontent.com/heretoholdbeer/HypnoControl-Downloads/main/downloads/",StringComparison.OrdinalIgnoreCase)) throw new Exception("Nicht oeffentlicher oder nicht vertrauenswuerdiger Download-Link.");
        if(!Regex.IsMatch(Str(d,"sha256"),@"^[0-9a-fA-F]{64}$") || Long(d,"size")<=0) throw new Exception("Ungueltige Dateipruefsumme.");
        return new Release{Tag=tag,Number=n,Url=Str(d,"url"),Sha=Str(d,"sha256").ToLowerInvariant(),Size=Long(d,"size")};
      }
    }
    // Anonymous users cannot access the private application repository.
    // Only public download metadata and public verified binaries are trusted.
    static Release FindLatest() { return FromManifest(); }
    static string SafePath(Release r) {
      if(!Regex.IsMatch(r.Tag,@"^v[0-9]+\.[0-9]+\.[0-9]+-alpha\.[0-9]+$"))throw new Exception("Ungueltiges Release-Tag");
      return Path.Combine(Versions,r.Tag,"HypnoControl.exe");
    }
    static string Hash(string path) {
      using(var f=File.OpenRead(path))using(var s=SHA256.Create())
        return BitConverter.ToString(s.ComputeHash(f)).Replace("-","").ToLowerInvariant();
    }
    static bool Good(string path, Release r) { return File.Exists(path) && new FileInfo(path).Length==r.Size && Hash(path)==r.Sha; }
    static string Installed() {
      if(!File.Exists(CurrentFile))return null;
      var tag=File.ReadAllText(CurrentFile).Trim();
      if(Number(tag)==null)return null;
      string file=Path.Combine(Versions,tag,"HypnoControl.exe");
      return File.Exists(file)?file:null;
    }
    static void Install(Release r) {
      Directory.CreateDirectory(Versions);
      string file=SafePath(r);
      if(!Good(file,r)) {
        string dir=Path.GetDirectoryName(file);
        Directory.CreateDirectory(dir);
        string tmp=Path.Combine(dir,"download-"+Guid.NewGuid().ToString("N")+".tmp");
        try {
          using(var w=Client()) w.DownloadFile(r.Url,tmp);
          if(!Good(tmp,r))throw new Exception("Download-Pruefsumme oder Dateigroesse stimmt nicht. Keine Installation erfolgt.");
          if(File.Exists(file))File.Delete(file);
          File.Move(tmp,file);
        } finally { if(File.Exists(tmp))File.Delete(tmp); }
      }
      string marker=CurrentFile+".tmp";
      File.WriteAllText(marker,r.Tag);
      if(File.Exists(CurrentFile))File.Delete(CurrentFile);
      File.Move(marker,CurrentFile);
      // Keep all previously installed versions for manual rollback; never touch user settings.
    }
    static void Launch() {
      string exe=Installed();
      if(exe==null)throw new Exception("Noch keine Anwendung installiert. Zuerst UPDATE auswaehlen.");
      Process.Start(new ProcessStartInfo(exe){WorkingDirectory=Path.GetDirectoryName(exe),UseShellExecute=true});
    }
    [STAThread] static int Main(string[] args) {
      ServicePointManager.SecurityProtocol=SecurityProtocolType.Tls12;
      if(args.Contains("--self-test")) {
        try {
          if(Number("v0.24.0-alpha.340").CompareTo(Number("v0.24.0-alpha.325"))<=0)return 1;
          if(Number("v0.24.0-alpha.999").CompareTo(Number("v0.24.0-alpha.340"))<=0)return 2;
          if(Number("../invalid")!=null)return 3;
          return 0;
        }catch{return 4;}
      }
      if(args.Contains("--verify-update")) {
        try {var r=FindLatest();Install(r);return Good(SafePath(r),r)?0:1;}catch(Exception e){Console.Error.WriteLine(e);return 2;}
      }
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new LauncherForm());
      return 0;
    }
    class LauncherForm:Form {
      Label status=new Label();Button update=new Button(),start=new Button(),check=new Button();
      bool busy;
      public LauncherForm() {
        Text="HypnoControl Launcher 2.0";Width=510;Height=255;StartPosition=FormStartPosition.CenterScreen;
        BackColor=Color.FromArgb(22,27,37);ForeColor=Color.White;Font=new Font("Segoe UI",10);
        var title=new Label{Text="HYPNOCONTROL",Font=new Font("Segoe UI",18,FontStyle.Bold),Left=24,Top=18,Width=440,Height=43};
        status.SetBounds(24,70,450,55);status.Text=Installed()==null?"Noch keine Version installiert.":"Installiert: "+Path.GetFileName(Path.GetDirectoryName(Installed()));
        update.Text="UPDATE / INSTALLIEREN";update.SetBounds(24,135,215,40);
        start.Text="APP STARTEN";start.SetBounds(252,135,215,40);
        check.Text="Auf neue Version pruefen";check.SetBounds(24,182,443,25);
        Controls.AddRange(new Control[]{title,status,update,start,check});
        update.Click+=(s,e)=>Run(true);check.Click+=(s,e)=>Run(false);
        start.Click+=(s,e)=>{try{Launch();}catch(Exception ex){MessageBox.Show(ex.Message);}};
        Shown+=(s,e)=>Run(true);
      }
      void SetStatus(string s){if(!IsDisposed)BeginInvoke((Action)(()=>status.Text=s));}
      void Run(bool install) {
        if(busy)return;busy=true;update.Enabled=false;check.Enabled=false;
        Task.Run(()=>{
          try {
            SetStatus("GitHub-Releases werden geprueft ...");
            var r=FindLatest();
            string installed=Installed();
            string local=installed==null?"keine":Path.GetFileName(Path.GetDirectoryName(installed));
            if(install) {
              if(Number(local)!=null && Number(local).CompareTo(r.Number)>0) SetStatus("Installiert: "+local+" (neuer als oeffentlicher Stand "+r.Tag+") – kein Downgrade");
              else if(local==r.Tag && Good(SafePath(r),r)) SetStatus("Installiert: "+r.Tag+" (aktuell) – APP STARTEN");
              else { SetStatus("Lade "+r.Tag+" und pruefe SHA-256 ..."); Install(r); SetStatus("Installiert: "+r.Tag+" – APP STARTEN"); }
            } else SetStatus("Installiert: "+local+" | Online: "+r.Tag+(local==r.Tag?" (aktuell)":" (Update verfuegbar)"));
          }catch(Exception e){SetStatus("Fehler: "+e.Message);}
          finally{if(!IsDisposed)BeginInvoke((Action)(()=>{busy=false;update.Enabled=true;check.Enabled=true;}));}
        });
      }
    }
  }
}
