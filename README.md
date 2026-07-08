# Missão Marte

A NASA vai pousar veículos na superfície de Marte. O campo de pouso é
retangular e mapeado como uma matriz, com o canto inferior esquerdo em `0 0`.

Cada veículo tem uma posição `X Y` e uma orientação (`N`, `L`, `S`, `O`). Para
controlá-lo, a NASA envia uma sequência de comandos:

- `E` — vira 90° à esquerda, sem sair do lugar
- `D` — vira 90° à direita, sem sair do lugar
- `A` — avança uma casa, mantendo a direção

O ponto ao Norte de `(x, y)` é `(x, y+1)`. Os veículos se movem em sequência: o
próximo só começa depois que o anterior termina.

## Entrada

- A primeira linha é o canto superior direito do campo (o inferior esquerdo é
  sempre `0 0`).
- Depois, cada veículo ocupa duas linhas: a posição inicial (`X Y Orientação`) e
  a sequência de comandos.

## Saída

Para cada veículo, as coordenadas e a orientação finais.

## Exemplo

Entrada:
```
5 5
1 2 N
EAEAEAEAA
3 3 L
AADAADADDA
```

Saída:
```
1 3 N
5 1 L
```

## Como executar

```bash
dotnet run
```

A entrada é informada pelo terminal, passo a passo. O campo é desenhado antes e
depois da movimentação de cada veículo.
