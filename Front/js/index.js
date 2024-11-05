function toggleMenu() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('active');
    updateContentMargin();
}

function updateContentMargin() {
    const sidebar = document.getElementById('sidebar');
    const contenido = document.querySelector('.contenido');

    if (sidebar.classList.contains('active')) {
        contenido.style.marginLeft = '350px'; // Ajusta según el ancho de tu sidebar
    } else {
        contenido.style.marginLeft = '0';
    }
}

document.addEventListener('click', function(event) {
    const sidebar = document.getElementById('sidebar');
    const botonMenu = document.querySelector('.boton-menu');

    // Verifica si el clic fue fuera del menú y el botón
    if (!sidebar.contains(event.target) && !botonMenu.contains(event.target)) {
        sidebar.classList.remove('active');
        updateContentMargin();
    }
});


//VERIFICAR LOGIN
const login={
    email: 'correo123@gmail.com',
    clave: 'clave123'
}

function verificar_login(){
    const $email = document.getElementById('emailInput')
    const $clave = document.getElementById('i-con')

    if($email.value === ''){
        alert('Debe ingresar un email!')
        return false
    }
    if($clave.value === ''){
        alert('Debe ingresar un contraseña!')
        return false
    }
    if($email.value != login.email || $clave.value != login.clave){
        alert('Email o contraseña incorrectas!')
        return false
    }

    window.location.href="/Index.html";
}

function MostrarContra(){
    const $pass = document.getElementById("i-con");
    $icon = document.querySelector(".bx");
    $icon.addEventListener("click", e => {
    if($pass.type === "password"){
    $pass.type = "text";
    }else{
    $pass.type = "password";
    }
    })
}


//VERIFICAR REGISTER
function verificar_register(){
    const $nomApe = document.getElementById("nomApe");
    const $email = document.getElementById('emailInput');
    const $usu = document.getElementById("usu")
    const $clave = document.getElementById('i-con');


    if($nomApe.value === ''){
        alert('Debe ingresar un Nombre y Apellido!');
        return false;
    }



    if($email.value === ''){
        alert('Debe ingresar un email!');
        return false;
    }
    if($usu.value === ''){
        alert('Debe ingresar un usuario!');
        return false;
    }


    if($clave.value === ''){
        alert('Debe ingresar un contraseña!');
        return false;
    }
    window.location.href="/Index.html";
}



const $mainContent = document.querySelector(".container-fluid");
const $row =document.querySelector(".row");
//Mostrar sección seleccionada
function mostrarSeccion(seccionId) {
    document.querySelectorAll(".seccion").forEach(seccion => {
        seccion.style.display = "none";
    });
    document.getElementById(seccionId).style.display = "block";
    if (seccionId === 'formularioTurno') {
        cargarServicio(); // Cargar servicio solo cuando se muestra la sección de formulario de turno
    }
}

async function cargarServicio() {
    try {
        const response = await fetch("https://localhost:7033/api/Servicio");
        const servicio = await response.json();
        const $servicios = document.getElementById("servicio");

        $servicios.innerHTML = '';

        servicio.forEach((element) => {
            const $option = document.createElement("option");
            $option.value = element.id;
            $option.textContent = element.nombre;
            $servicios.appendChild($option);
        })
    } catch (error) {
        console.error("Error al cargar los servicios:", error);
    }
}

async function nombreServicio(idServicio) {
    try {
        const response = await fetch(`https://localhost:7033/api/Servicio/${idServicio}`);
        const servicio = await response.json();
        return servicio.nombre;
    } catch (error) {
        console.error('Error: ', error);
    }
}


async function mostrarTabla(){
    const $tablaTurnos = document.getElementById("turnos");
    $tablaTurnos.innerHTML = "";
    try {
        const response = await fetch('https://localhost:7033/api/DetallesTurno');    
        const turnos = await response.json();

        console.log('Turnos fetched:', turnos);
        // Fetch all service names first
        const serviciosPromises = turnos.map(turno => nombreServicio(turno.idServicio));
        const serviciosNombres = await Promise.all(serviciosPromises);

        let row = '';
        turnos.forEach((turno, index) => {
            const nombreServicio = serviciosNombres[index];
            row += `
                <tr data-id="${turno.idTurnoNavigation.id}">
                    <td style="display: none;">${turno.idTurnoNavigation.id}</td>
                    <td>${nombreServicio}</td>
                    <td>${turno.idTurnoNavigation.cliente}</td>
                    <td>${turno.idTurnoNavigation.fecha}</td>
                    <td>${turno.idTurnoNavigation.hora}</td>
                    <td>
                        <button class="btn btn-danger" onclick="eliminarTurno(${turno.idTurnoNavigation.id}, ${turno.idServicio})">Eliminar</button>
                    </td>
                </tr>
            `;
        });
        $tablaTurnos.innerHTML = row;
        contarTurnosPorCliente(turnos)
    } catch (error) {
        console.error('Error: ', error);
    }
}

//Contar turnos por cliente
function contarTurnosPorCliente(turnos) {
    const clientes = {};
    turnos.forEach(turno => {
        if (clientes[turno.idTurnoNavigation.cliente]) {
            clientes[turno.idTurnoNavigation.cliente]++;
        } else {
            clientes[turno.idTurnoNavigation.cliente] = 1;
        }
    });
    console.log('Turnos por cliente:', clientes);
    // Llenar tabla de turnos por cliente
    const $tablaClientes = document.getElementById("clientes");
    $tablaClientes.innerHTML = "";
    let clienteIndex = 1;
    for (const cliente in clientes) {
        $tablaClientes.innerHTML += `
            <tr>
                <td>${clienteIndex}</td>
                <td>${cliente}</td>
                <td>${clientes[cliente]}</td>
            </tr>
        `;
        clienteIndex++;
    }
}


//Agregar un nuevo turno desde el formulario
async function agregarTurno() {
    const $servicio = document.getElementById("servicio");
    const $cliente = document.getElementById("cliente");
    const $fecha = document.getElementById("fecha");
    const $hora = document.getElementById("hora");

    if(!$servicio || !$cliente || !$fecha || !$hora) {
        alert('Todos los campos son obligatorios.');
        return;
    }

    const fechaSeleccionada = new Date($fecha +' '+ $hora);
    const ahora = new Date();

    if(fechaSeleccionada <= ahora) {
        alert('La fecha y hora no pueden ser menores a la fecha y hora actual.');
        return;
    }

    const nuevoTurno = {
        servicio: $servicio.value,
        cliente: $cliente.value,
        fecha: $fecha.value,
        hora: $hora.value,
    };
    try {
        const response = await fetch('https://localhost:7033/api/Turno', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(nuevoTurno)
        });
        console.log('Response status:', response.status);
        console.log('Response:', await response.clone().text());

        if (response.ok) {
            alert('Turno agregado exitosamente.');
            await mostrarTabla();
            document.getElementById("nuevoTurnoForm").reset();
        } else {
            const errorText = await response.text();
            console.error('Error:', errorText || 'No response text');
            throw new Error('Error al agregar el turno: ' + errorText || 'No response text');
        }
    } catch (error) {
        console.error('Error:', error.message);
        alert('No se pudo agregar el turno. Por favor intentelo más tarde.');
    }
}

//Eliminar turno
async function eliminarTurno(idTurno) {
    try {
        const response = await fetch(`https://localhost:7047/api/DetallesTurno/${idTurno}?`, {
            method: 'DELETE'
        });
        if(!response.ok) {
            throw new Error('Error al eliminar el turno: '+ response.statusText);
        }
        document.querySelector(`tr[data-id="${idTurno}"]`).remove();
        alert('Turno eliminado exitosamente.');
    } catch (error) {
        console.error('Error: ', error);
        alert('No se pudo eliminar el turno. Por favor intentelo más tarde');
    }
}

window.onload = mostrarTabla;