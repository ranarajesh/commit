FROM mcr.microsoft.com/mssql/server

COPY . /

RUN chmod +x /db-init.sh
CMD /bin/bash ./entrypoint.sh