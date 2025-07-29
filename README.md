# 🚀 Kafka Console App - Dual .NET Framework Support

אפליקציית קונסול לעבודה עם Apache Kafka הזמינה בשתי גרסאות:
- **.NET 8** (רץ על macOS/Linux/Windows)
- **.NET Framework 4.8** (רץ על Windows בלבד)

**שתי הגרסאות מכילות אותו קוד בדיוק - רק מותאמות לפלטפורמות שונות!**

---

## 📁 מבנה התיקיות

### 📂 `net8/` - גרסה מודרנית (.NET 8)
```
net8/
├── KafkaConsoleApp.csproj     # פרויקט .NET 8
├── Program.cs                 # הקוד הראשי 
├── appsettings.json           # הגדרות JSON
└── kafka-docker-compose.yml   # Docker עבור Kafka
```

**🟢 פועל על:**  
- ✅ macOS (Apple Silicon & Intel)
- ✅ Linux  
- ✅ Windows
- ✅ Docker

### 📂 `net48/` - גרסה מסורתית (.NET Framework 4.8)
```
net48/
├── KafkaConsoleApp.csproj     # פרויקט .NET Framework 4.8
├── Program.cs                 # אותו קוד בדיוק!
├── App.config                 # הגדרות XML
├── packages.config            # חבילות NuGet
└── Properties/
    └── AssemblyInfo.cs        # מידע Assembly
```

**🟠 פועל על:**  
- ✅ Windows בלבד
- ✅ Visual Studio 2017+
- ❌ macOS/Linux (לא נתמך)

---

## 🚀 איך להריץ

### NET8 (הגרסה המומלצת):
```bash
# 1. הפעל Kafka
cd net8
docker-compose -f kafka-docker-compose.yml up -d

# 2. הרץ את האפליקציה  
dotnet run
```

### NET Framework 4.8 (על Windows):
```cmd
REM 1. שחזר חבילות
cd net48
nuget restore

REM 2. בנה את הפרויקט
msbuild KafkaConsoleApp.csproj /p:Configuration=Release

REM 3. הרץ את האפליקציה
bin\Release\KafkaConsoleApp.exe
```

---

## 🎯 תכונות זהות בשתי הגרסאות

### Producer (שליחת הודעות) 📤
- שליחה אסינכרונית של הודעות
- מפתחות ייחודיים אוטומטיים
- וידוא משלוח (Acknowledgments) 
- טיפול בשגיאות

### Consumer (קבלת הודעות) 📥
- האזנה רציפה להודעות חדשות
- תמיכה ב-Consumer Groups
- ניהול Offsets ידני
- סגירה חלקה

### ממשק משתמש 🖥️
- ממשק בעברית וידידותי
- תפריט אינטראקטיבי
- הודעות מפורטות עם אמוג'י
- טיפול בשגיאות

---

## ⚙️ הגדרות

### NET8 - `appsettings.json`:
```json
{
  "KafkaBootstrapServers": "localhost:9092",
  "KafkaTopic": "test-topic"
}
```

### NET Framework 4.8 - `App.config`:
```xml
<appSettings>
  <add key="KafkaBootstrapServers" value="localhost:9092" />
  <add key="KafkaTopic" value="test-topic" />
</appSettings>
```

---

## 🔧 הבדלים טכניים

| היבט | NET8 | NET Framework 4.8 |
|------|------|-------------------|
| **הגדרות** | `appsettings.json` | `App.config` |
| **חבילות** | `PackageReference` | `packages.config` |
| **פרויקט** | SDK-Style | MSBuild Classic |
| **פלטפורמות** | Cross-platform | Windows Only |
| **Performance** | מהיר יותר | מהיר |
| **זיכרון** | פחות צריכה | יותר צריכה |

---

## 📋 דרישות מערכת

### NET8:
- .NET 8 SDK או חדש יותר
- Docker (אופציונלי, עבור Kafka)

### NET Framework 4.8:
- Windows 10/11 או Windows Server
- .NET Framework 4.8
- Visual Studio 2017+ או MSBuild Tools
- NuGet CLI

---

## 🧪 בדיקת תקינות

### בדוק שקפקא רץ:
```bash
# רשימת Topics
docker exec kafka kafka-topics --bootstrap-server localhost:9092 --list

# שלח הודעת בדיקה
docker exec -it kafka kafka-console-producer --broker-list localhost:9092 --topic test-topic

# קבל הודעות
docker exec -it kafka kafka-console-consumer --bootstrap-server localhost:9092 --topic test-topic --from-beginning
```

---

## 🤝 מי מעדיף איזה גרסה?

### 🌟 בחר NET8 אם:
- אתה עובד על macOS/Linux
- רוצה ביצועים מיטביים  
- מתכנן deploy לקלאוד
- רוצה תמיכה ארוכת טווח

### 🏢 בחר NET Framework 4.8 אם:
- עובד בסביבת Windows ארגונית
- יש לך קוד legacy קיים
- חייב תאימות לרכיבי Windows ישנים
- משתמש ב-IIS

---

**🎯 Bottom Line: שתי הגרסאות עושות בדיוק אותו דבר - רק על פלטפורמות שונות!** 