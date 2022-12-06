rem @echo off
REM Pacote necessario coverlet.msbuild  Version=3.2.0 no projeto de testes
mkdir coverage /p
del .\coverage\*.* /s/f/q
dotnet test ..\ViagemYamaha.sln -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura  -p:CoverletOutput=..\coverage\
reportgenerator -reports:".\coverage\coverage.cobertura.xml" -targetdir:".\coverage\report"  -reporttypes:Html
start .\coverage\report\index.html
