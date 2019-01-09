# HashValidator
Exemplo de aplicativo para cálculo de HASH de mensagem XML no padrão TISS.

# Informações
Aplicativo Angular padrão utilizando ASP NET CORE 2.2 (C#).

## Objetivo
O objetivo deste repositório é demonstrar uma maneira de calcular o HASH de mensagens XML´s trafegadas no padrão TISS conforme requisitos 
definidos pela ANS na documentação oficial disponível em 
http://www.ans.gov.br/prestadores/tiss-troca-de-informacao-de-saude-suplementar/padrao-tiss-dezembro-2017 (versão TISS 3.03.03).

## Exemplo
O aplicativo está publicado temporariamente no endereço https://hash-validator.herokuapp.com para eventuais testes. Ele também pode 
ser acessado via API com as orientações abaixo:
  * Endereço: http://hash-validator.herokuapp.com/api/hash
  * Verbo: POST
  * Body: Raw (Text)
  * Response: basta enviar a mensagem TISS completa no corpo da requisição que será retornar uma resposta JSON com o HASH informado, HASH calculado e o 
conteúdo extraído da mensagem para geração do HASH.
