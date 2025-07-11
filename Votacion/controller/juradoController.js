module.exports = (socket, io, habilitados) => {
socket.on("habilitar-papeleta", (votanteId) => {
habilitados[votanteId] = true;
io.emit("papeleta-habilitada", votanteId);
console.log(`Jurado habilitó a votante ${votanteId}`);
});
};