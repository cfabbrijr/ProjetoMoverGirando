using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverGirando : MonoBehaviour
{

    Vector3 objetivo; // define a variável objetivo, que vai guardar a localização alvo no nosso jogo.
    float velocidade = 1.0f; // define a variável velocidade do nosso jogador.
    float precisao = 1.0f; // define a precisão , uma medida de distância entre nosso jogador e o nosso local objetivo.
    float rotVelocidade = 2f; // define a velocidade de rotação do movimento.
    
    void Start()
    {
        objetivo = this.transform.position;

        // A variável objetivo vai definir nossa posição inicial no jogo.
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit impacto; 
        // RaycastHit nos ajuda a obter informação do raio colidindo com um objeto do jogo, no caso nosso plano.
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Nosso raio foi criado e a partir da posição da camera é disparado na direção do mouse e sua localização.

        if (Physics.Raycast(raio, out impacto) && Input.GetMouseButtonDown(0))
            // chamamos a física para nos ajudar: se a variavel impacto atingi o plano, essa coordenada é guardada na variavel
            // objetivo abaixo.  Avaliamos também se o botão esquerdo do mouse foi pressionado. Somente quando essas duas ações
            // acontecem é que queremos marcar a posição onde aconteceu
            {
                objetivo = new Vector3(impacto.point.x, this.transform.position.y, impacto.point.z);
                // Aqui as coordenadas x e z vem da variavel impacto e a coordenada y é a mesma do nosso cubo, para ficarmos na
                // mesma altura dele
            }

        Vector3 direção = objetivo - this.transform.position;
        // Aqui criamos um vetor chamado direção que nos dá a direção e distância entre o objetivo e a posição atual do cubo.

            if(Vector3.Distance(transform.position, objetivo) > precisao)
            // aqui enquanto o cubo está longe do nosso objetivo, vamos movimentá-lo, pelo código abaixo
            // Se ele já estiver dentro da precisão, pode parar.
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                    Quaternion.LookRotation(direção), Time.deltaTime * rotVelocidade);
                    // Aqui usamos o método de giro com rotação dado por Quaternion. Slerp
                    // Slerp usa o método de interpolação linear esférica. O outro método é o Lerp.
                    // Usamos a variavel direção para dizer pra onde o agente deve girar.
                    this.transform.Translate(0, 0, velocidade * Time.deltaTime);
                    // usamos o método translate aqui para executar a movimentação do cubo até o local que escolhemos no plano.
                    // as primeiras coordenadas, x e y, são zero pois não há movimento nessa direção. 
                    // a coordenada Z é que terá movimento multiplicando a velocidade pelo tempo da unity
                }
        
    }
}
