from django.urls import path
from votantes.views.votante_view import VotanteListCreateView, VotanteRetrieveUpdateDestroyView
from votantes.views.publico_view import VerificarPadronView

urlpatterns = [
    path('votantes/', VotanteListCreateView.as_view(), name='votantes-list-create'),
    path('votantes/<uuid:pk>/', VotanteRetrieveUpdateDestroyView.as_view(), name='votante-detail'),
    path('verificar/<str:ci>/', VerificarPadronView.as_view(), name='verificar-padron'),
]
