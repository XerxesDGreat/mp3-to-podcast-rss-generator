FROM mcr.microsoft.com/dotnet/runtime:6.0-alpine

RUN mkdir -p /home/podcast/files && \
    groupadd podcast -g 1000 && \
    useradd -m podcast -u 1000 -g 1000

COPY entrypoint.sh /home/podcast/entrypoint.sh
RUN chown podcast:podcast -R /home/podcast && \
    chmod +x /home/podcast/entrypoint.sh

WORKDIR /home/podcast

USER podcast

CMD ["bash", "/home/podcast/entrypoint.sh"]