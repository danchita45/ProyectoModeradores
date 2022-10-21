function mostrarModal() {
    $('#ImportFile').modal('show');
}
function mostrarModalNuevaSala() {
    $('#NuevaSala').modal('show');
}
function NuevoModModal() {
    $('#NuevoMod').modal('show');
}


function EditMod(obj) {
    document.getElementById('Id').value = obj.Id;
    document.getElementById('Name').value = obj.Name;
    document.getElementById('ApellidoP').value = obj.ApellidoP;
    document.getElementById('ApellidoM').value = obj.ApellidoM;
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