# HashValidator
Exemplo de aplicativo para cálculo de HASH de mensagem XML no padrão TISS.

## Informações
Aplicativo Angular padrão utilizando ASP NET CORE 2.2 (C#).

## Objetivo
O objetivo deste repositório é demonstrar uma maneira de calcular o HASH de mensagens XML´s trafegadas no padrão TISS conforme requisitos 
definidos pela ANS na documentação oficial disponível em 
http://www.ans.gov.br/prestadores/tiss-troca-de-informacao-de-saude-suplementar/padrao-tiss-dezembro-2017 (versão TISS 3.03.03).

O HASH é utilizado para garantir a integridade da mensagem pelo receptor e segundo a ANS seu cálculo deve obedecer aos seguintes critérios:

_No Padrão TISS o cálculo do hash deve considerar apenas a concatenação do conteúdo das tags desprezando as tags XML propriamente ditas. O conteúdo das tags deve ser concatenado e considerado de forma literal, desde o primeiro caracter à esquerda até o último caracter à direita, sem qualquer inserção, supressão, modificação ou ajuste, respeitando maiúsculas e minúsculas, pontuação, acentuação
e caracteres especiais, mesmo invisíveis que eventualmente existam (CR,LF,tabs etc). O encoding a ser utilizado será sempre o ISO-8859-1._

## Como usar
O aplicativo está publicado temporariamente no endereço https://hash-validator.herokuapp.com para eventuais testes. Ele também pode 
ser acessado via API com as orientações abaixo:
  * Endereço: http://hash-validator.herokuapp.com/api/hash
  * Verbo: POST
  * Body: Raw (Text)
  * Response: basta enviar a mensagem TISS completa no corpo da requisição que será retornar uma resposta JSON com o HASH informado, HASH calculado e o conteúdo extraído da mensagem para geração do HASH.
  
A principal premissa é de que a mensagem XML esteja bem formada, mas não se exige necessariamente que ela obedeça ao XSD publicado pela ANS (apesar disto ser altamente recomendado, obviamente).
