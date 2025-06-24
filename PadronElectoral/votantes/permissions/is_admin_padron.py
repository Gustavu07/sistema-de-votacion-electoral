from rest_framework.permissions import BasePermission

class IsAdminPadron(BasePermission):
    def has_permission(self, request, view):
        print("ROL:", getattr(request.user, 'rol', None))
        return getattr(request.user, 'rol', None) == "admin_padron"

