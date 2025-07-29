# פתרון לבעיית הדיבוג ב-Visual Studio 2022

## הבעיה
Visual Studio 2022 מציג שגיאה: "Visual Studio cannot start debugging because the debug target 'KafkaConsoleApp.exe' is missing"

## הסיבה
הפרויקט לא נבנה בהצלחה, ולכן קובץ ה-EXE לא נוצר.

## פתרונות

### פתרון 1: בניית הפרויקט מ-Visual Studio
1. פתח את Visual Studio 2022
2. פתח את הפרויקט `KafkaConsoleApp.csproj`
3. לחץ על `Build > Build Solution` (או Ctrl+Shift+B)
4. בדוק שהבנייה הצליחה ללא שגיאות
5. נסה להריץ שוב (F5)

### פתרון 2: שימוש בסקריפט הבנייה
1. פתח Command Prompt כ-Administrator
2. נווט לתיקיית הפרויקט: `cd "C:\Users\rotem\OneDrive\Desktop\net-kafaka\net48"`
3. הרץ את הסקריפט: `quick-build.bat`

### פתרון 3: Developer Command Prompt
1. פתח "Developer Command Prompt for VS 2022" מתפריט Start
2. נווט לתיקיית הפרויקט
3. הרץ: `msbuild KafkaConsoleApp.csproj /p:Configuration=Debug`

### פתרון 4: ניקוי ובנייה מחדש
1. ב-Visual Studio, לחץ על `Build > Clean Solution`
2. אחר כך `Build > Rebuild Solution`

## בדיקת התוצאה
לאחר בנייה מוצלחת, הקובץ `KafkaConsoleApp.exe` אמור להיות ב:
```
bin\Debug\KafkaConsoleApp.exe
```

## בעיות נפוצות
1. **NuGet packages חסרים**: הרץ `nuget restore` לפני הבנייה
2. **.NET Framework 4.7.2 לא מותקן**: הורד מאתר Microsoft
3. **Visual Studio Build Tools חסרים**: הורד מ-visualstudio.microsoft.com

## אם כלום לא עוזר
נסה ליצור פרויקט חדש:
1. File > New > Project
2. Console App (.NET Framework)
3. העתק את הקוד מהפרויקט הישן 