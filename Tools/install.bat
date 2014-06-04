copy .\Dnevnik.FileGenerator\bin\Release "%userprofile%\AppData\Roaming\Dnevnik\"
copy .\Dnevnik.Template\bin\Release\ItemTemplates\CSharp\1033\Dnevnik.Template.zip "%userprofile%\Documents\Visual Studio 2013\Templates\ItemTemplates\Visual C#"
C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm.exe /codebase "%userprofile%\AppData\Roaming\Dnevnik\Dnevnik.FileGenerator.dll"
