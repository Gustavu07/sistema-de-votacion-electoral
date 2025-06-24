# authentication.py
from rest_framework_simplejwt.authentication import JWTAuthentication
from rest_framework.exceptions import AuthenticationFailed

class ExternalUser:
    def __init__(self, user_id, username, rol):
        self.id = user_id
        self.username = username
        self.rol = rol
        self.is_authenticated = True

    def __str__(self):
        return self.username

class JWTNoUserAuthentication(JWTAuthentication):
    def authenticate(self, request):
        raw_token = self.get_raw_token(self.get_header(request))
        if raw_token is None:
            return None

        validated_token = self.get_validated_token(raw_token)

        user_id = validated_token.get("user_id")
        username = validated_token.get("username", "anon")
        rol = validated_token.get("rol")

        if not user_id or not rol:
            raise AuthenticationFailed("Token válido pero incompleto", code="invalid_token")

        user = ExternalUser(user_id, username, rol)
        return (user, validated_token)
