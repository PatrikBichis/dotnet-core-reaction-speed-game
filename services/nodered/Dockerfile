FROM nodered/node-red:latest
RUN for addonnodes in \
node-red-dashboard \
node-red-contrib-influxdb \
node-red-contrib-ui-led \
node-red-node-rbe \
node-red-node-tail \
; do \
npm install ${addonnodes} ;\
done;
