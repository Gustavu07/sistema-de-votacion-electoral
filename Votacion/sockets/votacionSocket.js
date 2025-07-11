const juradoController = require("../controller/juradoController");
const votoController = require("../controller/votoController");
const adminController = require("../controller/adminController");

module.exports = function (io) {
  const habilitados = {}; // Estado compartido entre sockets

  io.on("connection", (socket) => {
    console.log("Cliente conectado:", socket.id);

    // Conectamos cada controlador con su lógica
    juradoController(socket, io, habilitados);
    votoController(socket, io, habilitados);
    adminController(socket, io, habilitados);

    socket.on("disconnect", () => {
      console.log("Cliente desconectado:", socket.id);
    });
  });
};
