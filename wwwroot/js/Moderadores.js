function mostrarModal() {
    $('#ImportFile').modal('show');
}
function mostrarModalNuevaSala() {
    $('#NuevaSala').modal('show');
}
function NuevoModModal() {
    $('#NuevoMod').modal('show');

}function nuevoevento() {
    $('#NuevoMod').modal('show');
}


function EditMod(obj) {
    document.getElementById('Id').value = obj.Id;
    document.getElementById('NameId').value = obj.Name;
    document.getElementById('email').value = obj.email;
    document.getElementById('Area1').value = obj.Area1;
    document.getElementById('Area2').value = obj.Area2;
    document.getElementById('InstitucionId').value = obj.InstitucionId;
    $('#NuevoMod').modal('show');
}


function EditSala(obj) {
    document.getElementById('Id').value = obj.SalaId;
    document.getElementById('Code').value = obj.Code;
    document.getElementById('Desc').value = obj.Description;
    document.getElementById('Status').value = obj.StatusId;
    document.getElementById('Area').value = obj.AreaId;
    $('#NuevaSala').modal('show');
}

function EditEvent(obj) {
    document.getElementById('Id').value = obj.Id;
    document.getElementById('Code').value = obj.Code;
    document.getElementById('Name').value = obj.Name;
    document.getElementById('Desc').value = obj.Description;
    document.getElementById('Status').value = obj.StatusId;
    document.getElementById('Area').value = obj.AreaId;
    document.getElementById('Moderador').value = obj.ModeradorId;
    $('#NuevoMod').modal('show');
}