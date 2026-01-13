using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class NotificacionHub : Hub
    {
        // Cuando el usuario se conecta, lo agregamos a un grupo con su userId
        // para poder enviarle notificaciones específicas
        public async Task RegistrarUsuario(int usuarioId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"usuario_{usuarioId}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}