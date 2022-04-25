using CurrieTechnologies.Razor.SweetAlert2;
using ExamenTercerParcial.Interfaces;
using Microsoft.AspNetCore.Components;
using Modelos;

namespace ExamenTercerParcial.Pages.Usuarios;

partial class EditarUsuario
{
    [Inject] private IUsuarioServicio _usuarioServicio { get; set; }

    [Inject] NavigationManager _navigationManager { get; set; }
    [Inject] SweetAlertService Swal { get; set; }

    [Parameter] public string Codigo { get; set; }

    Usuario user = new Usuario();

    protected override async Task OnInitializedAsync()
    {
        if (!string.IsNullOrEmpty(Codigo))
        {
            user = await _usuarioServicio.GetPorCodigo(Codigo);
        }
    }

    protected async Task Guardar()
    {
        if (string.IsNullOrEmpty(user.Codigo) || string.IsNullOrEmpty(user.Nombre) || string.IsNullOrEmpty(user.Rol) || user.Rol == "Seleccionar")
        {
            return;
        }

        bool edito = await _usuarioServicio.Actualizar(user);
        if (edito)
        {
            await Swal.FireAsync("Felicidades", "Usuario actualizado con exito", SweetAlertIcon.Success);
        }
        else
        {
            await Swal.FireAsync("Error", "Usuario no se pudo actualizar", SweetAlertIcon.Error);
        }
        _navigationManager.NavigateTo("/Usuarios");

    }

    protected async Task Cancelar()
    {
        _navigationManager.NavigateTo("/Usuarios");
    }

    protected async Task Eliminar()
    {
        bool elimino = false;

        SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
        {
            Title = "¿Seguro que quiere eliminar el registro?",
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            ConfirmButtonText = "Aceptar",
            CancelButtonText = "Cancelar"
        });

        if (!string.IsNullOrEmpty(result.Value))
        {
            elimino = await _usuarioServicio.Eliminar(user);

            if (elimino)
            {
                await Swal.FireAsync("Felicidades", "Usuario eliminado con exito", SweetAlertIcon.Success);
                _navigationManager.NavigateTo("/Usuarios");
            }
            else
            {
                await Swal.FireAsync("Error", "Usuario no se pudo eliminar", SweetAlertIcon.Error);
            }
        }
    }
}

enum Roles
{
    Seleccionar,
    Administrador,
    Usuario
}




