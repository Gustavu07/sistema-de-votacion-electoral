from rest_framework.views import APIView
from rest_framework.response import Response
from votantes.models.votante import Votante

class VerificarPadronView(APIView):
    permission_classes = []

    def get(self, request, ci):
        try:
            votante = Votante.objects.get(ci=ci)
            return Response({
                "ci": votante.ci,
                "nombre_completo": votante.nombre_completo,
                "recinto": votante.recinto.nombre if votante.recinto else None
            })
        except Votante.DoesNotExist:
            return Response({"error": "No registrado"}, status=404)
