function doc(el){
    $('main form *').val('')
    var cpf = ('#cpf')

    if($(el).val()== cpf)
    {
        alert('cadastro de usuario "Pessoa física"')
    }
}

function mascaraCpf(mascara, input) {
    const vetMask = mascara.split("")
    const numCpf = input.value.replace(/\D/g, "")
    const cursor = Number(input.selectionStart)
    const tecla = (window.event) ? event.keyCode : event.which

    for(let i=0; i<numCpf.length; i++) {
        vetMask.splice(vetMask.indexOf("_"), 1, numCpf[i])
    }

    input.value = vetMask.join("")

    if(tecla != 37 && (cursor == 3 || cursor == 7 || cursor == 11)) {
        input.setSelectionRange(cursor+1, cursor+1)
    } else {
        input.setSelectionRange(cursor, cursor)
    }

}

function Campos (el,er)
{
    var e=$ (el).val().replace(er,'');
}

function maskCpf(el)
{
 Campos(el,/[^0-9\.\-]/g)
 var e=$ (el).val();
 if(event.keyCode!=8)
 {
     if(e.length==3)
     $(el).val(e+".")
     else if (e.length==7)
     $(el).val(e+".")
     else if (e.length==11)
     $(el).val(e+"-")
 }
}  