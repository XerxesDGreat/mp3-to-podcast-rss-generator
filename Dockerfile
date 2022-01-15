FROM mcr.microsoft.com/dotnet/sdk:latest

RUN mkdir -p /home/podcast/files && \
    mkdir -p /home/podcast/app && \
    groupadd podcast -g 1000 && \
    useradd -m podcast -u 1000 -g 1000

COPY mvc /home/podcast/app/mvc
COPY src /home/podcast/app/src

COPY entrypoint.sh /home/podcast/entrypoint.sh
RUN chown podcast:podcast -R /home/podcast && \
    chmod +x /home/podcast/entrypoint.sh

WORKDIR /home/podcast/app

USER podcast

CMD ["bash", "/home/podcast/entrypoint.sh"]