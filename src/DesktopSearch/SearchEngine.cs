using System.Text;
using System.Text.RegularExpressions;
namespace DesktopSearch;

public sealed record SearchOptions(string Root,string Query,bool Content=false,string? Extension=null,bool CaseSensitive=false,int MaxResults=100,long MaxContentBytes=1_048_576,bool IncludeHidden=false);
public sealed record SearchHit(string Path,long Size,DateTime ModifiedUtc,string Match);
public sealed record SearchReport(IReadOnlyList<SearchHit> Hits,int FilesScanned,int FilesSkipped,IReadOnlyList<string> Errors);

public sealed class SearchEngine {
 public SearchReport Search(SearchOptions o){
  if(string.IsNullOrWhiteSpace(o.Query)) throw new ArgumentException("Query cannot be empty.");
  if(o.MaxResults<1) throw new ArgumentOutOfRangeException(nameof(o.MaxResults));
  var root=Path.GetFullPath(o.Root); if(!Directory.Exists(root)) throw new DirectoryNotFoundException(root);
  var hits=new List<SearchHit>(); var errors=new List<string>(); int scanned=0,skipped=0;
  var cmp=o.CaseSensitive?StringComparison.Ordinal:StringComparison.OrdinalIgnoreCase;
  var pending=new Stack<string>(); pending.Push(root);
  while(pending.Count>0 && hits.Count<o.MaxResults){ var dir=pending.Pop();
   try { foreach(var sub in Directory.EnumerateDirectories(dir)){ if(o.IncludeHidden || !IsHidden(sub)) pending.Push(sub); } } catch(Exception ex) when(ex is UnauthorizedAccessException or IOException){errors.Add($"{dir}: {ex.Message}");}
   try { foreach(var file in Directory.EnumerateFiles(dir)){ if(hits.Count>=o.MaxResults) break; if(!o.IncludeHidden&&IsHidden(file)){skipped++;continue;} if(o.Extension is not null&&!file.EndsWith(NormalizeExt(o.Extension),cmp)){skipped++;continue;} scanned++; var info=new FileInfo(file); var rel=Path.GetRelativePath(root,file); string? match=null;
     if(rel.Contains(o.Query,cmp)) match="name"; else if(o.Content && info.Length<=o.MaxContentBytes && IsProbablyText(file)){ try{var text=File.ReadAllText(file,Encoding.UTF8); if(text.Contains(o.Query,cmp)) match="content";}catch(Exception ex) when(ex is IOException or UnauthorizedAccessException or DecoderFallbackException){errors.Add($"{rel}: {ex.Message}");}}
     if(match is not null) hits.Add(new(rel,info.Length,info.LastWriteTimeUtc,match));
   }} catch(Exception ex) when(ex is UnauthorizedAccessException or IOException){errors.Add($"{dir}: {ex.Message}");}
  }
  return new(hits.OrderBy(h=>h.Path,StringComparer.OrdinalIgnoreCase).ToList(),scanned,skipped,errors);
 }
 static string NormalizeExt(string e)=>e.StartsWith('.')?e:"."+e;
 static bool IsHidden(string p){try{return (File.GetAttributes(p)&FileAttributes.Hidden)!=0 || Path.GetFileName(p).StartsWith('.');}catch{return true;}}
 static bool IsProbablyText(string p){string[] binary={".png",".jpg",".jpeg",".gif",".webp",".pdf",".zip",".gz",".7z",".exe",".dll",".bin",".mp3",".mp4",".mov",".avi"}; return !binary.Contains(Path.GetExtension(p),StringComparer.OrdinalIgnoreCase);}
}