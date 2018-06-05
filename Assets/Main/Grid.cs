using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MainInGame
{
    public class Grid : MonoBehaviour
    {
        public GameManager gm;

        private Dictionary<string, OneHit> points;
        private Dictionary<string, GameObject> edges;

        // cellPrefabs: 0:неактивно 1:серый 2:синий 3:красный 4:зелёный
        public List<Sprite> pointsPrefabs;
        // cellPrefabs: 0:серый 1:синий 2:красный 3:зелёный
        public List<Sprite> edgesPrefabs;

        public GameObject[] lastHitedPoints;

        // Эталон точек
        public GameObject mainCell;
        // Эталон рёбер
        public GameObject mainEdge;
        // контейнер рёбер
        public GameObject edgesList;
        // контейнер точек
        public GameObject pointsList;

        public int blackCells;  // толщина пустых ячеек вокруг занятых




        private void Start()
        {
            OneHit.grd = this;
            BoCoCell.grd = this;
            points = new Dictionary<string, OneHit>();
            edges = new Dictionary<string, GameObject>();

            // Создаём новую точку в 000 координатах и заполняем полупрозрачное поле вокруг неё
            Point zp = new Point(0, 0);
            AddBlackField( zp );

            GameObject zpogo = FindPoint(zp).gameObject;
            lastHitedPoints = new GameObject[3] { zpogo, zpogo, zpogo };

            OnStart();
        }
        private void Update()
        {
            OnUpdate();
        }




        /*
         *      Для каждой добавленной ячейки проверяем существуют ли 3 рядом стоящие точки в списке точек
         *      если да - добавляем ребро, иначе - кладём болт
         * */
        private void AddBlackField(Point point)
        {
            for (int x = -blackCells; x <= blackCells; x++)
                for (int y = -blackCells; y <= blackCells; y++)
                {
                    // чтобы получался красивый шестиугольник из тыщи точег
                    if( Math.Abs(x-y) > blackCells)
                        continue;

                    Point newPoint = new Point( point.x + x, point.y + y );

                    // если текущая точка не центр ячейки и если точки не существует
                    if( newPoint.GetSystCoordOrientation()!=2 && FindPoint(newPoint)==null ) {
                        OneHit 
                            oh = NewPoint(newPoint),
                            ch;

                        // проверяем наличие рядомстоящих точек и добавляем рёбра
                        ch = FindPoint( newPoint.GetAround(1) );
                        if( ch != null )
                            NewEdge( oh, ch );
                        ch = FindPoint( newPoint.GetAround(5) );
                        if( ch != null )
                            NewEdge( oh, ch );
                        ch = FindPoint( newPoint.GetAround(9) );
                        if( ch != null )
                            NewEdge( oh, ch );
                    }
                }
        }

        public OneHit NewPoint(Point point)
        {
            GameObject newEl = Instantiate(mainCell, point.GetCoord2D(), Quaternion.identity);
            newEl.name = point.ToString();
            newEl.transform.SetParent( pointsList.transform );

            OneHit ohp = newEl.GetComponent<OneHit>();

            ohp.Initialize(point);
            points.Add(newEl.name, ohp);

            return newEl.GetComponent<OneHit>();
        }

        public GameObject NewEdge(OneHit p1, OneHit p2)
        {
            Vector3 edgeVector = p2.transform.position-p1.transform.position;

            // находим угол через скалярное произведение вектора оси координат х и edgeVector
            // когда так и не вкурил кватернионы((((((((((((((((((((((((((
            float
                cosa = edgeVector.x / edgeVector.magnitude,
                signAsina = Mathf.Asin( edgeVector.y / edgeVector.magnitude ) > 0 ? 1 : -1,
                degree = 180f/Mathf.PI * signAsina * Mathf.Acos( edgeVector.x / edgeVector.magnitude );
            Vector3 edgePosition = edgeVector/2 + p1.transform.position;

            GameObject newEdge = Instantiate( mainEdge, edgePosition, Quaternion.Euler(0,0,degree) );
            
            newEdge.name = p1.coord.ToString() + "^" + p2.coord.ToString();

            newEdge.transform.SetParent(edgesList.transform);
            edges.Add(newEdge.name, newEdge);

            return newEdge;
        }





        // совершить ход в данную точку. Финальная - завершительная часть хода.
        public bool MakeHit(OneHit point, int player)
        {
            //player: 1 2 3
            // проверить занята ли точка, либо случилась победа
            if (point.state != 1)
                return false;
            
            point.whoShodil = player;
            // Обновить точку, выше всех остальных шагов чтобы пользователь видел что произошёл ход
            // 1:2 2:3 3:4
            point.SetState(player+1);

            // В последнюю схоженную данным игроком ячейку помещаем "иконку последнего хода" этого игрока
            lastHitedPoints[player - 1] = point.gameObject;

            //if (FindPobedaInPoint(point.coord, player))
            //{
                //return true;
            //}

            // создать чёрное поле
            AddBlackField(point.coord);

            return true;
        }



        public OneHit FindPoint(Point point)
        {
            return FindPointStr(point.ToString());
        }
        // Есть куски кода где надо искать по строке а не по точке
        public OneHit FindPointStr(string pstr)
        {
            if (!points.ContainsKey(pstr))
                return null;

            return points[pstr];
        }


        
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
    }
};