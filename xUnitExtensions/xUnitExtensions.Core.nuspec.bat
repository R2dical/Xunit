for /f %%x in ('wmic path win32_localtime get /format:list ^| findstr "="') do set %%x
set version=%Year%.%Month%.%Day%.1
nuget pack -Version %version% -Symbols -Properties Configuration=Release