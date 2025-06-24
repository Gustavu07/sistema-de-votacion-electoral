from rest_framework import serializers
from votantes.models.votante import Votante
from votantes.serializers.recinto_serializer import RecintoSerializer

class VotanteSerializer(serializers.ModelSerializer):
    recinto = RecintoSerializer(read_only=True)
    recinto_id = serializers.PrimaryKeyRelatedField(
        queryset=Votante._meta.get_field('recinto').related_model.objects.all(),
        source='recinto',
        write_only=True
    )

    class Meta:
        model = Votante
        fields = [
            'id',
            'ci',
            'nombre_completo',
            'direccion',
            'foto_anverso',
            'foto_reverso',
            'foto_rostro',
            'recinto',
            'recinto_id'
        ]
