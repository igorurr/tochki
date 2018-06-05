using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MainInGame
{
    public class OneHit : MonoBehaviour {
	    public static Grid grd;
        public static GameManager gm;
        public BoCoCell bcc;   // задаётся в инспекторе юнити

        public int state;			// state: -1:неактивно 0:пусто 1:синий 2:красный 3:зелёный 4синийWin 5:крассныйWin
	    public int whoShodil;		// кто сходил в данную клетку? 0-никто 1-синий 2-красный 3-зелёный

	    public Point coord; // Трёхмерные координаты данной точки
        public OneHit[] around;

        // Мышка была нажата, потенциально можем сделать ход в ячейку,
        // если она не покинула ячейку, как только покинет будет false
        public bool mouseDidDown;

        /*
            *  Жизненный цикл объекта OneHit, при создании приобретает указаные координаты и состояние 0
            *  После хода в точку данного объекта меняет состояние в зависимости от того кто сходил
            *  Если победная линия проходит через данную точку, приобретает победный префаб и меняет угол
            * */
	    void Start()
        {
            // не работай здесь через старт вообще - нахуй его, реализовывай Create и вызывай там где надо
            // start работает в другом потоке походу
        }
        public void Initialize( Point coord ){
		    this.coord = coord;
            SetState(1);
        }




        
	    public void SetState( int newState ) {
		    GetComponent<SpriteRenderer>().sprite = grd.pointsPrefabs[newState];
		    state = newState;
	    }



	    public void OnMouseDown() {
		    if ( grd.gm.cam.CheckClickInUI() )
			    return;
			
		    mouseDidDown = true;
	    }
	    public void OnMouseExit() {
		    if (!mouseDidDown)
			    return;

            grd.gm.cam.StartMovie();

		    mouseDidDown = false;
	    }
	    public void OnMouseUp() {
            //Debug.Log (string.Format ( "x:{0} y:{1} z:{2} st:{3} pr:", coord.x, coord.y, coord.z, state, pref.ToString() ));
            if ( mouseDidDown )
                grd.gm.MakeHitLocalPlayer( this );

            grd.gm.cam.StopMovie();

		    mouseDidDown = false;
	    }
    }
};