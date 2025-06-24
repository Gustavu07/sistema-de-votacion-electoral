from rest_framework import serializers
from votantes.models.recinto import Recinto

class RecintoSerializer(serializers.ModelSerializer):
    class Meta:
        model = Recinto
        fields = ['id', 'nombre', 'direccion']
