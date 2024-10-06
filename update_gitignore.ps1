Set-Location (Split-Path -Parent $MyInvocation.MyCommand.Path)
if((Test-Path .gitignore) -eq "True") {
    Remove-Item -Force .gitignore~
    Move-Item .gitignore .gitignore~
}
Get-Content -Encoding UTF8 gitignore_list.txt | ForEach-Object {
    (Invoke-WebRequest "https://github.com/github/gitignore/raw/HEAD/${_}.gitignore").Content >> .gitignore
}
Get-Content -Encoding UTF8 gitignore_local.txt >> .gitignore
