# Desktop Search

A dependency-free **.NET 8** command-line file finder for fast, private local searches. Search file names, optionally inspect small text files, filter by extension, cap results, and emit JSON for automation. Nothing is uploaded anywhere.

## English
### Why
Desktop Search provides a predictable, script-friendly alternative when you need a portable search tool without an index, cloud service, database, or third-party runtime package.

### Features
- Recursive filename search, case-insensitive by default.
- Optional UTF-8 text-content search with a configurable per-file size ceiling.
- Extension, hidden-file, case-sensitivity and result-limit controls.
- Skips common binary formats during content inspection.
- Relative result paths, file size, modification time and match source.
- JSON output and meaningful exit codes for scripts.
- Handles inaccessible paths as warnings instead of crashing the whole scan.
- Zero NuGet runtime dependencies; no telemetry or network calls.

### Requirements & installation
Install the .NET 8 SDK. Clone the repository, then:
```bash
dotnet build DesktopSearch.sln -c Release
dotnet run --project src/DesktopSearch -- --query report --root .
```
Publish a standalone app for your current runtime with `dotnet publish src/DesktopSearch -c Release`.

### Usage
```bash
# filename search
dotnet run --project src/DesktopSearch -- --query invoice --root ~/Documents
# filename + text content, only Markdown
dotnet run --project src/DesktopSearch -- --query TODO --root . --content --ext md
# automation-friendly output
dotnet run --project src/DesktopSearch -- --query config --root . --json
```
Options: `--content`, `--ext EXT`, `--case-sensitive`, `--hidden`, `--max N` (default 100), `--max-content-bytes N` (default 1 MiB), `--json`, `--version`, `--help`.

### Configuration
There is no config file or environment variable. Every behavior is explicit on the command line.

### Project structure
- `src/DesktopSearch/` — CLI and search engine.
- `tests/DesktopSearch.Tests/` — dependency-free end-to-end checks.
- `.github/workflows/ci.yml` — Windows/Linux/macOS build and test matrix.

### Testing
```bash
dotnet run --project tests/DesktopSearch.Tests/DesktopSearch.Tests.csproj -c Release
```
The tests create an isolated temporary directory and verify name matching, content matching, extension filtering, case sensitivity, and result limits.

### Preview / screenshots
This is a terminal application. For a repository preview, capture `--help` and a search against a disposable sample folder; do not publish screenshots containing private file paths.

### Security & privacy
Searches remain local. Content search reads only files below the configured size limit and avoids common binary extensions. Permission/I/O failures are reported as warnings. Do not run with privileges you do not need. See `SECURITY.md`.

### Limitations
This release intentionally performs a live scan: it has no persistent index, fuzzy search, regex syntax, filesystem watcher, GUI, or binary-document parsing. Content matching assumes UTF-8-compatible text and does not follow file contents inside archives/PDF/Office documents.

### Optional roadmap
A persistent opt-in index, glob filters, and a desktop UI are possible future additions; none are claimed as current features.

### Contributing
See `CONTRIBUTING.md`. MIT licensed; see `LICENSE`.

## العربية
### نظرة عامة
**Desktop Search** أداة سطر أوامر مبنية على .NET 8 للبحث المحلي والخاص في الملفات دون فهرس دائم أو خدمة سحابية أو حزم تشغيل خارجية. تبحث في أسماء الملفات، ويمكنها اختياريًا البحث داخل الملفات النصية الصغيرة مع فلاتر واضحة ومخرجات JSON للأتمتة.

### لماذا المشروع؟
يوفر طريقة قابلة للتوقع وسهلة للسكربتات عندما تحتاج بحثًا مباشرًا في مجلد بدون رفع البيانات أو تشغيل قاعدة بيانات أو خدمة خلفية.

### الميزات
- بحث تكراري في أسماء الملفات وغير حساس لحالة الأحرف افتراضيًا.
- بحث اختياري داخل محتوى UTF-8 مع حد أقصى قابل للتعديل لحجم الملف.
- فلترة بالامتداد، والتحكم بالملفات المخفية وحالة الأحرف وعدد النتائج.
- تجنب أشهر الصيغ الثنائية أثناء فحص المحتوى.
- عرض المسار النسبي والحجم ووقت التعديل ومصدر التطابق.
- JSON ورموز خروج مناسبة للأتمتة.
- أخطاء الصلاحيات والإدخال/الإخراج تظهر كتحذيرات بدل إيقاف الفحص بالكامل.
- لا Telemetry ولا اتصالات شبكة ولا اعتماديات NuGet وقت التشغيل.

### المتطلبات والتثبيت
ثبّت .NET 8 SDK ثم:
```bash
dotnet build DesktopSearch.sln -c Release
dotnet run --project src/DesktopSearch -- --query report --root .
```

### أمثلة الاستخدام
```bash
dotnet run --project src/DesktopSearch -- --query invoice --root .
dotnet run --project src/DesktopSearch -- --query TODO --root . --content --ext md
dotnet run --project src/DesktopSearch -- --query config --root . --json
```
الخيارات الأساسية: `--content` و`--ext` و`--case-sensitive` و`--hidden` و`--max` و`--max-content-bytes` و`--json`.

### الإعداد والبنية
لا يوجد ملف إعداد أو متغيرات بيئة؛ السلوك يحدد من سطر الأوامر. الكود في `src/DesktopSearch/`، والاختبارات في `tests/DesktopSearch.Tests/`، وCI في `.github/workflows/ci.yml`.

### الاختبار
```bash
dotnet run --project tests/DesktopSearch.Tests/DesktopSearch.Tests.csproj -c Release
```
تتحقق الاختبارات من البحث بالاسم والمحتوى والامتداد وحالة الأحرف وحد النتائج باستخدام مجلد مؤقت معزول.

### الخصوصية والأمان
كل البحث محلي. فحص المحتوى مقيد بحجم الملف ويتجنب صيغًا ثنائية شائعة. لا تشغّل الأداة بصلاحيات أعلى من حاجتك. راجع `SECURITY.md`.

### القيود
لا يوجد حاليًا فهرس دائم أو بحث تقريبي أو Regex أو مراقبة مباشرة لنظام الملفات أو واجهة رسومية أو قراءة محتوى PDF/Office/الأرشيفات. البحث النصي مخصص للنصوص المتوافقة مع UTF-8.

### تطوير اختياري
يمكن مستقبلًا إضافة فهرس اختياري وفلاتر glob وواجهة سطح مكتب، وهي ليست ميزات موجودة حاليًا.

### المساهمة والترخيص
راجع `CONTRIBUTING.md`. المشروع مرخص MIT؛ راجع `LICENSE`.

## Author / المؤلف
**Radwan Abdulhadi Ahmed**  
**رضوان عبدالهادي أحمد**  
GitHub: **@rad03i2**
