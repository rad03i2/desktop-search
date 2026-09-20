using DesktopSearch;
var root=Path.Combine(Path.GetTempPath(),"desktop-search-tests-"+Guid.NewGuid()); Directory.CreateDirectory(root);
try{
 File.WriteAllText(Path.Combine(root,"notes.txt"),"alpha needle omega"); File.WriteAllText(Path.Combine(root,"Needle-name.md"),"nothing"); File.WriteAllText(Path.Combine(root,"ignore.log"),"needle");
 var e=new SearchEngine();
 Assert(e.Search(new(root,"needle")).Hits.Count==1,"name search");
 Assert(e.Search(new(root,"needle",Content:true)).Hits.Count==3,"content search");
 var filtered=e.Search(new(root,"needle",Content:true,Extension:"txt")); Assert(filtered.Hits.Count==1&&filtered.Hits[0].Match=="content","extension filter");
 Assert(e.Search(new(root,"NEEDLE",Content:true,CaseSensitive:true)).Hits.Count==0,"case-sensitive search");
 Assert(e.Search(new(root,"needle",Content:true,MaxResults:1)).Hits.Count==1,"result limit");
 Console.WriteLine("All DesktopSearch tests passed."); return 0;
} finally {Directory.Delete(root,true);}
static void Assert(bool ok,string name){if(!ok)throw new Exception("Test failed: "+name);}