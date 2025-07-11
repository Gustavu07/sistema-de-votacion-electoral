module.exports = (socket, io, votos) => {
socket.on("resumen-votos", () => {
const conteo = {};

javascript
Copiar
Editar
votos.forEach((v) => {
  conteo[v.candidaturaId] = (conteo[v.candidaturaId] || 0) + 1;
});

socket.emit("resumen-votos", conteo);
});
};