using System.Text.Json;
using DesktopSearch;

if(args.Length==0 || args.Contains("--help")){Help();return 0;}
if(args.Contains("--version")){Console.WriteLine("desktop-search 1.0.0");return 0;}
try{
 string root=Value("--root")??Directory.GetCurrentDirectory(); string query=Value("--query")??throw new ArgumentException("--query is required");
 bool content=args.Contains("--content"); bool cs=args.Contains("--case-sensitive"); bool hidden=args.Contains("--hidden"); bool json=args.Contains("--json");
 string? ext=Value("--ext"); int max=int.TryParse(Value("--max"),out var n)?n:100; long maxBytes=long.TryParse(Value("--max-content-bytes"),out var b)?b:1_048_576;
 var report=new SearchEngine().Search(new(root,query,content,ext,cs,max,maxBytes,hidden));
 if(json) Console.WriteLine(JsonSerializer.Serialize(report,new JsonSerializerOptions{WriteIndented=true})); else {foreach(var h in report.Hits) Console.WriteLine($"[{h.Match}] {h.Path}  ({h.Size} bytes)"); Console.WriteLine($"\n{report.Hits.Count} hit(s), {report.FilesScanned} scanned, {report.FilesSkipped} skipped, {report.Errors.Count} error(s)."); foreach(var e in report.Errors) Console.Error.WriteLine("warning: "+e);}
 return report.Errors.Count>0?2:0;
}catch(Exception ex){Console.Error.WriteLine("error: "+ex.Message);return 1;}
string? Value(string key){var i=Array.IndexOf(args,key);return i>=0&&i+1<args.Length?args[i+1]:null;}
void Help()=>Console.WriteLine("""desktop-search — fast local file search
Usage: desktop-search --query TEXT [--root PATH] [--content] [--ext EXT] [--case-sensitive] [--hidden] [--max N] [--max-content-bytes N] [--json]
Exit codes: 0 success, 1 invalid/fatal error, 2 completed with inaccessible-file warnings.
""");