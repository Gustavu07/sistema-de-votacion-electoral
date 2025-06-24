import uuid
from django.db import models
from .recinto import Recinto

class Votante(models.Model):
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    ci = models.CharField(max_length=20, unique=True)
    nombre_completo = models.CharField(max_length=150)
    direccion = models.TextField()
    foto_anverso = models.ImageField(upload_to='carnets/anverso/')
    foto_reverso = models.ImageField(upload_to='carnets/reverso/')
    foto_rostro = models.ImageField(upload_to='rostros/')
    recinto = models.ForeignKey(Recinto, on_delete=models.SET_NULL, null=True)

    def __str__(self):
        return f"{self.nombre_completo} ({self.ci})"
