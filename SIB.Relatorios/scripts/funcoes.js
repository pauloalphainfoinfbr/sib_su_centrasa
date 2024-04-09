/* ======================================================================
FUNCTION: 	DataMask

INPUT:		campo
				
RETURNS:		

DESC:			Deve ser usada em conjunto com o evento onKeyUp em um input
				do tipo text. A cada número digitado a função conta o tamanho
				atual da string. Se for 2 ou 5 ela inclui uma "/".
====================================================================== */

function DataMask(campo){
	var digits="0123456789/";
	var temp;
	var a;
	for (var i=0;i<campo.value.length;i++)
	{
		temp=campo.value.substring(i,i+1);
		if (digits.indexOf(temp)==-1){
			campo.value=campo.value.substring(0,(campo.value.length-1))
		}
		else{
			var texto=""
			if ((campo.value.length==2)||(campo.value.length==5)){
				texto=campo.value + "/"
				campo.value=texto
			}		
		}
	}
}
