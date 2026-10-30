@echo off
chcp 65001 > nul
set "MYSQL_LOCAL=%LOCALAPPDATA%\FecapStudy"
if not exist "%MYSQL_LOCAL%\mysql\bin\mysqld.exe" (
    echo MySQL local nao encontrado. Inicie o MySQL instalado neste computador.
    echo Configure a conexao em FecapStudy\Backend\mysql_config.txt.
    pause
    exit /b 1
)
"%MYSQL_LOCAL%\mysql\bin\mysqld.exe" --basedir="%MYSQL_LOCAL%\mysql" --datadir="%MYSQL_LOCAL%\mysql-dados" --bind-address=127.0.0.1 --port=3306 --mysqlx=OFF --console
pause
