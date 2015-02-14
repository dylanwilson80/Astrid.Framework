@echo off
git clone -b gh-pages https://github.com/craftworkgames/Astrid.Framework.git gh-pages
rmdir /s /q .\gh-pages\documentation
mkdir .\gh-pages\documentation
mkdir Documentation
echo "\r\n" | \SharpDox\SharpDox.Core.exe -config .\Astrid.Framework.sdox
xcopy .\Documentation\Html\default .\gh-pages\documentation /E
cd gh-pages
git add --all .\documentation
git commit -m "updated documentation"
git push
