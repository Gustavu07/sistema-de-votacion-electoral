module.exports = (socket, io, habilitados, votos) => {
socket.on("emitir-voto", ({ votanteId, candidaturaId }) => {
if (!habilitados[votanteId]) {
return socket.emit("error-voto", "Papeleta no habilitada");
}

javascript
Copiar
Editar
votos.push({ candidaturaId, timestamp: new Date().toISOString() });
delete habilitados[votanteId];

io.emit("nuevo-voto", { candidaturaId });
io.emit("papeleta-cerrada", votanteId);
console.log(`✅ Voto registrado para candidatura ${candidaturaId}`);
});
};