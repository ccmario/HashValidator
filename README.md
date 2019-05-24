# HashValidator
Exemplo de aplicativo para cálculo de HASH de mensagem XML no padrão TISS.

## Informações
Aplicativo Angular padrão utilizando ASP NET CORE 2.2.

## Objetivo
A finalidade deste repositório é demonstrar uma maneira de se calcular o HASH de mensagens XML´s trafegadas no padrão TISS conforme requisitos definidos pela ANS na documentação oficial disponível em 
http://www.ans.gov.br/prestadores/tiss-troca-de-informacao-de-saude-suplementar/padrao-tiss-fevereiro-2019.

## HASH MD-5

Um hash é uma sequência de bits gerada por um algoritmo de dispersão que permite a visualização em letras e números, representando 1/2 byte cada. O conceito teórico diz que “hash é a transformação de uma grande quantidade de informações em uma pequena quantidade de informações”.

Na comunicação TISS, o HASH é utilizado para garantir a integridade da mensagem e segundo a ANS seu cálculo deve obedecer aos seguintes critérios:

_No Padrão TISS o cálculo do hash deve considerar apenas a concatenação do conteúdo das tags desprezando as tags XML propriamente ditas. O conteúdo das tags deve ser concatenado e considerado de forma literal, desde o primeiro caracter à esquerda até o último caracter à direita, sem qualquer inserção, supressão, modificação ou ajuste, respeitando maiúsculas e minúsculas, pontuação, acentuação
e caracteres especiais, mesmo invisíveis que eventualmente existam (CR,LF,tabs etc). O encoding a ser utilizado será sempre o ISO-8859-1._

## Como usar
O aplicativo está publicado temporariamente no endereço https://hash-validator.herokuapp.com para eventuais testes. Ele também pode 
ser acessado via API com as orientações abaixo:
  * Endereço: http://hash-validator.herokuapp.com/api/hash
  * Verbo: POST
  * Body: Raw (Text)
  * Response: basta enviar a mensagem TISS completa no corpo da requisição que será retornada uma resposta JSON com o HASH informado, HASH calculado e o conteúdo extraído da mensagem que foi considerado no cálculo do HASH.
  
A principal premissa é de que a mensagem XML esteja bem formada, mas não se exige necessariamente que ela obedeça ao XSD publicado pela ANS (apesar disto ser, obviamente, altamente recomendado).
