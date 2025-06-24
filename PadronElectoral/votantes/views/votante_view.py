from rest_framework import generics, permissions
from votantes.models.votante import Votante
from votantes.serializers.votante_serializer import VotanteSerializer
from votantes.permissions import IsAdminPadron

class VotanteListCreateView(generics.ListCreateAPIView):
    queryset = Votante.objects.all()
    serializer_class = VotanteSerializer
    permission_classes = [permissions.IsAuthenticated, IsAdminPadron]

class VotanteRetrieveUpdateDestroyView(generics.RetrieveUpdateDestroyAPIView):
    queryset = Votante.objects.all()
    serializer_class = VotanteSerializer
    permission_classes = [permissions.IsAuthenticated, IsAdminPadron]
