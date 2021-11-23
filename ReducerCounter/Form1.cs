using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroSet_UI.Forms;
using MetroSet_UI.Interfaces;

namespace ReducerCounter
{
    //2131234
    public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
            
            //дефолтное значение переключателя режима расчета
            radioButton1.Checked = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
             // размеры окна
            Width = 1347;
            Height = 773;
        }


        public void Beginning() //ввод исх. данных
        {
            
            Parameters.ExPower = Double.Parse(ExPower_text.Text);

            
            Parameters.ExCircles = Int32.Parse(ExCircles_text.Text);
        }
        public void KPDCounting() // расчет кпд
        {

            
            Parameters.nu1 = double.Parse(nu1text.Text);

           
            Parameters.nu2 = double.Parse(nu2text.Text);

            
            Parameters.nu3 = double.Parse(nu3text.Text);

            
            Parameters.nu4 = double.Parse(nu4text.Text);

            
            Parameters.n = Int32.Parse(n_text.Text);

            

            Parameters.KPD = Parameters.nu1 * (Math.Pow(Parameters.nu2, Parameters.n)) * Parameters.nu3 * Parameters.nu4;
            output_text.Text = $"Общий КПД привода будет равен: {Parameters.KPD} = {Parameters.KPD * 100}%"; 
        }

        public void edPower() //расчет мощности ЭД, после расчета выбираем ближайшее табличное значение
        {
            Parameters.edPower = Parameters.ExPower / Parameters.KPD;
            output_text.Text += $"Первичная требуемая мощность электродвигателя равна {Parameters.edPower} кВт" + Environment.NewLine;

            switch (Parameters.edPower)
            {
                case < 0.37:
                    Parameters.edPower = 0.37;
                    break;

                case < 0.55:
                    Parameters.edPower = 0.55;
                    break;

                case < 0.75:
                    Parameters.edPower = 0.75;
                    break;

                case < 1.1:
                    Parameters.edPower = 1.1;
                    break;

                case < 1.5:
                    Parameters.edPower = 1.5;
                    break;

                case < 2.2:
                    Parameters.edPower = 2.2;
                    break;

                case < 3:
                    Parameters.edPower = 3;
                    break;

                case < 4:
                    Parameters.edPower = 4;
                    break;

                case < 5.5:
                    Parameters.edPower = 5.5;
                    break;

                case < 7.5:
                    Parameters.edPower = 7.5;
                    break;

                case < 11:
                    Parameters.edPower = 11;
                    break;

                case < 15:
                    Parameters.edPower = 15;
                    break;

                case < 18.5:
                    Parameters.edPower = 18.5;
                    break;

                case < 22:
                    Parameters.edPower = 22;
                    break;

                case < 30:
                    Parameters.edPower = 30;
                    break;
            }
            output_text.Text += $"Окончательная требуемая мощность электродвигателя равна {Parameters.edPower} кВт" + Environment.NewLine;
        }
        public void AllNumber() //расчет общего числа передач привода
        {
            
            Parameters.Per = Int32.Parse(Per_text.Text);

            if (Parameters.Per < 10 || Parameters.Per > 40)
            {
                MessageBox.Show("Ошибка! Введите данные заново");
            }
            else
            {
                Parameters.Klin = Int32.Parse(Klin_text.Text);

                if (Parameters.Klin < 2 || Parameters.Klin > 5)
                {
                    MessageBox.Show("Ошибка! Введите данные заново");
                    //return;
                }
                else
                {
                    Parameters.Freq = Double.Parse(Freq_text.Text);//синхронная частота

                    Parameters.u = Parameters.Freq / Parameters.ExCircles;

                    Parameters.PerPr = Parameters.Freq / Parameters.ExCircles; // передаточное число привода

                    Parameters.PerPr1 = Parameters.Per * Parameters.Klin;

                    Parameters.difference = Parameters.PerPr1 - Parameters.PerPr;

                    if (Parameters.difference < Parameters.PerPr1 * 0.1) //условие вывода передаточного отношения
                    {
                        output_text.Text += $"Ваше передаточное отношение редуктора равно {Parameters.PerPr1}" + Environment.NewLine;
                        output_text.Text += $"Вал двигателя: {Parameters.Freq}" + Environment.NewLine;
                        output_text.Text += $"Ведомый вал: {Parameters.ExCircles}" + Environment.NewLine;
                        Parameters.LeadingChervVal = Parameters.ExCircles * 2;
                        output_text.Text += $"Обороты на ведущем червячном валу равны {Parameters.LeadingChervVal} об/мин" + Environment.NewLine;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка! Введите данные заново");
                        //return;
                    }
                }
            }
        }

        public void KinematicCounter() // кинематический расчет
        {
            if (Parameters.difference < Parameters.PerPr1 * 0.1)
            {
                Parameters.om3 = (Math.PI * Parameters.ExCircles) / 30;
                output_text.Text += $"Omega3 = {Parameters.om3}" + Environment.NewLine;

                Parameters.om2 = Parameters.om3 * Parameters.Klin;
                output_text.Text += $"Omega2 = {Parameters.om2}" + Environment.NewLine;

                Parameters.om1 = (Math.PI * Parameters.Freq) / 30;
                output_text.Text += $"Omega 1 = {Parameters.om1}" + Environment.NewLine;

                //MessageBox.Show("Рассчитаем вращающие моменты Т1, Т2, Т3:...");

                Parameters.T3 = (Parameters.ExPower * 1000) / Parameters.om3;
                output_text.Text += $"Вращающий момент Т3 равен {Parameters.T3} Н*мм" + Environment.NewLine;

                Parameters.T2 = (Parameters.ExPower * 1000) / (Parameters.om2 * Parameters.nu3 * Math.Pow(Parameters.nu2, Parameters.n));
                output_text.Text += $"Вращающий момент Т2 равен {Parameters.T2} Н*мм" + Environment.NewLine;

                Parameters.T1 = (Parameters.ExPower * 1000) / (Parameters.om1 * Parameters.nu1 * Math.Pow(Parameters.nu2, Parameters.n));
                output_text.Text += $"Вращающий момент Т1 равен {Parameters.T1} Н*мм" + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("Ошибка! Введите данные заново");
            }
           
        }

        public void ZubchatieWheels() // расчет зубчатых колес
        {
            output_text.Text += $"Время безотказной работы принимаем равным {Parameters.timeOfWork} часов" + Environment.NewLine;
            output_text.Text += $"Температуру окружающей среды принимаем равной {Parameters.outsideTemp} градусов Цельсия" + Environment.NewLine;

            //MessageBox.Show("Выберем коэффициент высоты головки зуба h^a:...");
            Parameters.ha = Int32.Parse(ha_text.Text);

            //MessageBox.Show("Выберем коэффициент радиального зазора с*:...");
            Parameters.c = Double.Parse(c_text.Text);

            //MessageBox.Show("Выберем угол профиля рейки:... ");
            Parameters.alpha = Int32.Parse(alpha_text.Text);

            //MessageBox.Show("Выберите тип бронзы для венца червячного колеса: 1 - Оловянистые, 2 - Безоловянистые");
            //MessageBox.Show("Введите нужную цифру: 1 или 2");

            

            switch (bronze_text.Text)
            { //проверить по справочнику значения рядов
                case "1":
                    //MessageBox.Show("Введите значение SIGMA_Ha...");
                    Parameters.SIGMA_Ha = Int32.Parse(SIGMA_Ha_text.Text);

                    //MessageBox.Show("Введите значение SIGMA_F...");
                    Parameters.SIGMA_F = Int32.Parse(SIGMA_F_text.Text);

                    Parameters.type = 1;
                    break;

                case "2":
                    //MessageBox.Show("Введите значение SIGMA_Ha...");
                    Parameters.SIGMA_Ha1 = Int32.Parse(SIGMA_Ha_text.Text);

                    //MessageBox.Show("Введите значение SIGMA_F...");
                    Parameters.SIGMA_F1 = Int32.Parse(SIGMA_F_text.Text);

                    Parameters.type = 2;

                    break;

                default:
                    MessageBox.Show("Вы ввели несоответствующий номер операции");
                    break;
            }
        }

        public void AllowedTensions()
        {
            Parameters.n1 = (int)(Parameters.u * Parameters.LeadingChervVal);
            //MessageBox.Show("Рассчитаем ориентировочное значение скорости скольжения Vs:...");
            Parameters.Vs = 4.5 * Math.Pow(10, -4) * Parameters.n1 * Math.Pow(Parameters.T2, 1.0 / 3.0);
            output_text.Text += $"Значение скорости скольжения Vs = {Parameters.Vs} м/c" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем общее число циклов нагружения червячного колеса N2:...");
            Parameters.N2 = 60 * Parameters.timeOfWork * Parameters.LeadingChervVal;
            output_text.Text += $"Общее число циклов нагружения червячного колеса N2 равно {Parameters.N2}" + Environment.NewLine;

            //MessageBox.Show("Выберите коэффициенты приведения Khe и Kfe по соотвествующим режимам работы");
            //MessageBox.Show("Режимы работы определяются нажатием на клавиши 0-5");
            

            switch (resume_text.Text)
            {
                case "0":
                    Parameters.Kfe = 1.0;
                    Parameters.Khe = 1.0;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                case "1":
                    Parameters.Kfe = 0.2;
                    Parameters.Khe = 0.416;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                case "2":
                    Parameters.Kfe = 0.1;
                    Parameters.Khe = 0.2;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                case "3":
                    Parameters.Kfe = 0.04;
                    Parameters.Khe = 0.171;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                case "4":
                    Parameters.Kfe = 0.016;
                    Parameters.Khe = 0.081;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                case "5":
                    Parameters.Kfe = 0.004;
                    Parameters.Khe = 0.034;
                    output_text.Text += $"Ваши параметры: Kfe = {Parameters.Kfe}, Khe = {Parameters.Khe}" + Environment.NewLine;
                    break;

                default:
                    MessageBox.Show("Вы ввели несоответствующий номер операции");
                    break;
            }

            //MessageBox.Show("Вычислим эквивалентное число циклов нагружения по контактным напряжениям Nhe2:...");
            Parameters.Nhe2 = Parameters.N2 * Parameters.Khe;
            output_text.Text += $"Число циклов нагружения по контактным напряжениям Nhe2 = {Parameters.Nhe2}" + Environment.NewLine;

            //MessageBox.Show($"Вычислим эквивалентное число циклов нагружения по напряжениям изгиба Nfe2:...");
            Parameters.Nfe2 = Parameters.N2 * Parameters.Kfe;
            output_text.Text += $"Эквивалентное число циклов нагружения по напряжениям изгиба Nfe2 = {Parameters.Nfe2}" + Environment.NewLine;

            //MessageBox.Show("Проверим допускаемое контактное напряжение SIGMA_Ha");

            if (Parameters.type == 1)
            {
                Parameters.SIGMA_Ha2 = 0.9 * Parameters.SIGMA_F * Math.Pow((Math.Pow(10, 7) / Parameters.Nhe2), 1.0 / 8.0);
                output_text.Text += $"При проверке выявилось следующее значение SIGMA_Ha = {Parameters.SIGMA_Ha2}" + Environment.NewLine;
                if (Parameters.SIGMA_Ha2 >= (Parameters.SIGMA_F * 0.55) && Parameters.SIGMA_Ha2 <= (0.95 * Parameters.SIGMA_F))
                {
                    MessageBox.Show($"Проверка условия попадания в рекомендованный диапазон пройдена");
                }
                else
                {
                    MessageBox.Show($"Проверка условия попадания в рекомендованный диапазон не пройдена");
                    return;
                }
            }
            else if (Parameters.type == 2)
            {
                Parameters.SIGMA_Ha2 = 0.9 * Parameters.SIGMA_F1 * Math.Pow((Math.Pow(10, 7) / Parameters.Nhe2), 1.0 / 8.0);
                if (Parameters.SIGMA_Ha2 >= (Parameters.SIGMA_F1 * 0.55) && Parameters.SIGMA_Ha2 <= (0.95 * Parameters.SIGMA_F1))
                {
                    
                }
                else
                {
                    MessageBox.Show($"Проверка условия попадания в рекомендованный диапазон не пройдена");
                }
            }
        }

        public void InterAxial() // определение величины межосевого расстояния
        {
            if (Parameters.PerPr1 > 8 && Parameters.PerPr1 < 14)
            {
                Parameters.z1 = 4;
                output_text.Text += $"Число заходов червяка при передаточном отношении {Parameters.PerPr1} равно 4" + Environment.NewLine;
                //MessageBox.Show("Рассчитаем количество зубьев колеса при выбранном числе заходов червяка:...");
                Parameters.z2 = Parameters.z1 * Parameters.PerPr1;
                output_text.Text += $"Количество зубьев колеса равно {Parameters.z2}" + Environment.NewLine;

                //MessageBox.Show("Проведем предварительный расчет межосевого расстояния Aw:...");
                Parameters.Aw = 5 * Math.Pow((Math.Pow(170 / (Parameters.SIGMA_Ha2 * 4), 2) * 1.2 * Parameters.T2 * 1000), 1.0 / 3.0);
                output_text.Text += $"Значение Aw = {Parameters.Aw} мм, выполним его проверку со стандартными рядами и выведем сверенное значение:..." + Environment.NewLine;

                switch (aw_text.Text)
                {
                    case "1":
                        switch (Parameters.Aw)
                        {
                            case < 40:
                                Parameters.Aw = 40;
                                break;

                            case < 50:
                                Parameters.Aw = 50;
                                break;

                            case < 63:
                                Parameters.Aw = 63;
                                break;

                            case < 80:
                                Parameters.Aw = 80;
                                break;

                            case < 100:
                                Parameters.Aw = 100;
                                break;

                            case < 125:
                                Parameters.Aw = 125;
                                break;

                            case < 160:
                                Parameters.Aw = 160;
                                break;

                            case < 200:
                                Parameters.Aw = 200;
                                break;

                            case < 250:
                                Parameters.Aw = 250;
                                break;

                            case < 315:
                                Parameters.Aw = 315;
                                break;

                            case < 400:
                                Parameters.Aw = 400;
                                break;

                            case < 500:
                                Parameters.Aw = 500;
                                break;

                            case < 630:
                                Parameters.Aw = 630;
                                break;

                            case < 800:
                                Parameters.Aw = 800;
                                break;

                            case < 1000:
                                Parameters.Aw = 1000;
                                break;

                            case < 1250:
                                Parameters.Aw = 1250;
                                break;

                            case < 1600:
                                Parameters.Aw = 1600;
                                break;

                            case < 2000:
                                Parameters.Aw = 2000;
                                break;

                            case < 2500:
                                Parameters.Aw = 2500;
                                break;
                        }
                        break;

                    case "2":
                        switch (Parameters.Aw)
                        {
                            case < 71:
                                Parameters.Aw = 71;
                                break;

                            case < 90:
                                Parameters.Aw = 90;
                                break;

                            case < 112:
                                Parameters.Aw = 112;
                                break;

                            case < 140:
                                Parameters.Aw = 140;
                                break;

                            case < 180:
                                Parameters.Aw = 180;
                                break;

                            case < 224:
                                Parameters.Aw = 224;
                                break;

                            case < 280:
                                Parameters.Aw = 280;
                                break;

                            case < 355:
                                Parameters.Aw = 355;
                                break;

                            case < 450:
                                Parameters.Aw = 450;
                                break;

                            case < 560:
                                Parameters.Aw = 560;
                                break;

                            case < 710:
                                Parameters.Aw = 710;
                                break;

                            case < 900:
                                Parameters.Aw = 900;
                                break;

                            case < 1120:
                                Parameters.Aw = 1120;
                                break;

                            case < 1400:
                                Parameters.Aw = 1400;
                                break;

                            case < 1800:
                                Parameters.Aw = 1800;
                                break;

                            case < 2240:
                                Parameters.Aw = 2240;
                                break;
                        }
                        break;

                    default:
                        MessageBox.Show("Невходящее значение Aw");
                        break;
                }

                output_text.Text += $"Конечное значение межосевого расстояния Aw = {Parameters.Aw} мм" + Environment.NewLine;
            }
            else if (Parameters.PerPr1 > 14 && Parameters.PerPr1 < 30)
            {
                Parameters.z1 = 2;
                output_text.Text += $"Число заходов червяка при передаточном отношении {Parameters.PerPr1} равно 2" + Environment.NewLine;
                //MessageBox.Show("Рассчитаем количество зубьев колеса при выбранном числе заходов червяка:...");
                Parameters.z2 = Parameters.z1 * Parameters.PerPr1;
                output_text.Text += $"Количество зубьев колеса равно {Parameters.z2}" + Environment.NewLine;

                //MessageBox.Show("Проведем предварительный расчет межосевого расстояния Aw:...");
                Parameters.Aw = 5 * Math.Pow((Math.Pow(170 / (Parameters.SIGMA_Ha2 * 4), 2) * 1.2 * Parameters.T2 * 1000), 1.0 / 3.0);
                output_text.Text += $"Значение Aw = {Parameters.Aw} мм, выполним его проверку со стандартными рядами и выведем сверенное значение:..." + Environment.NewLine;

                switch (aw_text.Text)
                {
                    case "1":
                        switch (Parameters.Aw)
                        {
                            case < 40:
                                Parameters.Aw = 40;
                                break;

                            case < 50:
                                Parameters.Aw = 50;
                                break;

                            case < 63:
                                Parameters.Aw = 63;
                                break;

                            case < 80:
                                Parameters.Aw = 80;
                                break;

                            case < 100:
                                Parameters.Aw = 100;
                                break;

                            case < 125:
                                Parameters.Aw = 125;
                                break;

                            case < 160:
                                Parameters.Aw = 160;
                                break;

                            case < 200:
                                Parameters.Aw = 200;
                                break;

                            case < 250:
                                Parameters.Aw = 250;
                                break;

                            case < 315:
                                Parameters.Aw = 315;
                                break;

                            case < 400:
                                Parameters.Aw = 400;
                                break;

                            case < 500:
                                Parameters.Aw = 500;
                                break;

                            case < 630:
                                Parameters.Aw = 630;
                                break;

                            case < 800:
                                Parameters.Aw = 800;
                                break;

                            case < 1000:
                                Parameters.Aw = 1000;
                                break;

                            case < 1250:
                                Parameters.Aw = 1250;
                                break;

                            case < 1600:
                                Parameters.Aw = 1600;
                                break;

                            case < 2000:
                                Parameters.Aw = 2000;
                                break;

                            case < 2500:
                                Parameters.Aw = 2500;
                                break;
                        }
                        break;

                    case "2":
                        switch (Parameters.Aw)
                        {
                            case < 71:
                                Parameters.Aw = 71;
                                break;

                            case < 90:
                                Parameters.Aw = 90;
                                break;

                            case < 112:
                                Parameters.Aw = 112;
                                break;

                            case < 140:
                                Parameters.Aw = 140;
                                break;

                            case < 180:
                                Parameters.Aw = 180;
                                break;

                            case < 224:
                                Parameters.Aw = 224;
                                break;

                            case < 280:
                                Parameters.Aw = 280;
                                break;

                            case < 355:
                                Parameters.Aw = 355;
                                break;

                            case < 450:
                                Parameters.Aw = 450;
                                break;

                            case < 560:
                                Parameters.Aw = 560;
                                break;

                            case < 710:
                                Parameters.Aw = 710;
                                break;

                            case < 900:
                                Parameters.Aw = 900;
                                break;

                            case < 1120:
                                Parameters.Aw = 1120;
                                break;

                            case < 1400:
                                Parameters.Aw = 1400;
                                break;

                            case < 1800:
                                Parameters.Aw = 1800;
                                break;

                            case < 2240:
                                Parameters.Aw = 2240;
                                break;
                        }
                        break;

                    default:
                        MessageBox.Show("Невходящее значение Aw");
                        break;
                }

                output_text.Text += $"Конечное значение межосевого расстояния Aw = {Parameters.Aw} мм" + Environment.NewLine;
            }
            else if (Parameters.PerPr1 > 30)
            {
                Parameters.z1 = 1;
                output_text.Text += $"Число заходов червяка при передаточном отношении {Parameters.PerPr1} равно 1" + Environment.NewLine;
                //MessageBox.Show("Рассчитаем количество зубьев колеса при выбранном числе заходов червяка:...");
                Parameters.z2 = Parameters.z1 * Parameters.PerPr1;
                output_text.Text += $"Количество зубьев колеса равно {Parameters.z2}" + Environment.NewLine;

                //MessageBox.Show("Проведем предварительный расчет межосевого расстояния Aw:...");
                Parameters.Aw = 5 * Math.Pow((Math.Pow(170 / (Parameters.SIGMA_Ha2 * 4), 2) * 1.2 * Parameters.T2 * 1000), 1.0 / 3.0);
                output_text.Text += $"Значение Aw = {Parameters.Aw} мм, выполним его проверку со стандартными рядами и выведем сверенное значение:..." + Environment.NewLine;

                switch (aw_text.Text)
                {
                    case "1":
                        switch (Parameters.Aw)
                        {
                            case < 40:
                                Parameters.Aw = 40;
                                break;

                            case < 50:
                                Parameters.Aw = 50;
                                break;

                            case < 63:
                                Parameters.Aw = 63;
                                break;

                            case < 80:
                                Parameters.Aw = 80;
                                break;

                            case < 100:
                                Parameters.Aw = 100;
                                break;

                            case < 125:
                                Parameters.Aw = 125;
                                break;

                            case < 160:
                                Parameters.Aw = 160;
                                break;

                            case < 200:
                                Parameters.Aw = 200;
                                break;

                            case < 250:
                                Parameters.Aw = 250;
                                break;

                            case < 315:
                                Parameters.Aw = 315;
                                break;

                            case < 400:
                                Parameters.Aw = 400;
                                break;

                            case < 500:
                                Parameters.Aw = 500;
                                break;

                            case < 630:
                                Parameters.Aw = 630;
                                break;

                            case < 800:
                                Parameters.Aw = 800;
                                break;

                            case < 1000:
                                Parameters.Aw = 1000;
                                break;

                            case < 1250:
                                Parameters.Aw = 1250;
                                break;

                            case < 1600:
                                Parameters.Aw = 1600;
                                break;

                            case < 2000:
                                Parameters.Aw = 2000;
                                break;

                            case < 2500:
                                Parameters.Aw = 2500;
                                break;
                        }
                        break;

                    case "2":
                        switch (Parameters.Aw)
                        {
                            case < 71:
                                Parameters.Aw = 71;
                                break;

                            case < 90:
                                Parameters.Aw = 90;
                                break;

                            case < 112:
                                Parameters.Aw = 112;
                                break;

                            case < 140:
                                Parameters.Aw = 140;
                                break;

                            case < 180:
                                Parameters.Aw = 180;
                                break;

                            case < 224:
                                Parameters.Aw = 224;
                                break;

                            case < 280:
                                Parameters.Aw = 280;
                                break;

                            case < 355:
                                Parameters.Aw = 355;
                                break;

                            case < 450:
                                Parameters.Aw = 450;
                                break;

                            case < 560:
                                Parameters.Aw = 560;
                                break;

                            case < 710:
                                Parameters.Aw = 710;
                                break;

                            case < 900:
                                Parameters.Aw = 900;
                                break;

                            case < 1120:
                                Parameters.Aw = 1120;
                                break;

                            case < 1400:
                                Parameters.Aw = 1400;
                                break;

                            case < 1800:
                                Parameters.Aw = 1800;
                                break;

                            case < 2240:
                                Parameters.Aw = 2240;
                                break;
                        }
                        break;

                    default:
                        MessageBox.Show("Невходящее значение Aw");
                        break;
                }
                output_text.Text += $"Конечное значение межосевого расстояния Aw = {Parameters.Aw} мм" + Environment.NewLine;
            }
            else
            {
                MessageBox.Show("Ошибка! Введите значения заново.");
            }
        }

        public void Module() // определение значения модуля (сокращенный интервал списка модулей)
        {
            //MessageBox.Show("Определим значение модуля m согласно ГОСТ 9563-80");
            Parameters.m = 1.6 * (Parameters.Aw / Parameters.z2);
            output_text.Text += $"Первичное значение модуля m  = {Parameters.m}" + Environment.NewLine;

            switch (module_text.Text)
            {
                case "1":
                    switch (Parameters.m)
                    {
                        case < 1:
                            Parameters.m = 1;
                            break;

                        case < 1.25:
                            Parameters.m = 1.25;
                            break;

                        case <= 1.6:
                            Parameters.m = 1.6;
                            break;

                        case <= 2:
                            Parameters.m = 2;
                            break;

                        case <= 2.5:
                            Parameters.m = 2.5;
                            break;

                        case <= 3.15:
                            Parameters.m = 3.15;
                            break;

                        case <= 4:
                            Parameters.m = 4;
                            break;

                        case <= 5:
                            Parameters.m = 5;
                            break;

                        case <= 6.3:
                            Parameters.m = 6.3;
                            break;

                        case <= 8:
                            Parameters.m = 8;
                            break;

                        case <= 10:
                            Parameters.m = 10;
                            break;
                    }
                    break;

                case "2":
                    switch (Parameters.m)
                    {
                        case <= 1.12:
                            Parameters.m = 1.12;
                            break;

                        case <= 1.4:
                            Parameters.m = 1.4;
                            break;

                        case <= 1.8:
                            Parameters.m = 1.8;
                            break;

                        case <= 2.24:
                            Parameters.m = 2.24;
                            break;

                        case <= 2.8:
                            Parameters.m = 2.8;
                            break;

                        case <= 3.55:
                            Parameters.m = 3.55;
                            break;

                        case <= 4.5:
                            Parameters.m = 4.5;
                            break;

                        case <= 5.6:
                            Parameters.m = 5.6;
                            break;

                        case <= 7.1:
                            Parameters.m = 7.1;
                            break;

                        case <= 9:
                            Parameters.m = 9;
                            break;

                        case <= 11.2:
                            Parameters.m = 11.2;
                            break;
                    }
                    break;

                default:
                    MessageBox.Show("Невходящее значение m может находиться в полноразмерном интервале значений по ГОСТ 9563-80");
                    break;
            }
            output_text.Text += $"Выбранное значение модуля m согласно стандартному ряду равно: {Parameters.m}" + Environment.NewLine;

            //MessageBox.Show("Уточним коэффициент диаметра червяка q согласно ГОСТ 19672-74");
            Parameters.q = (2 * Parameters.Aw - (Parameters.z2 * Parameters.m)) / Parameters.m;
            output_text.Text += $"Рассчетное значение q = {Parameters.q}. Выберем ближайшее к нему из стандартных рядов ГОСТ 19672-74" + Environment.NewLine;

            switch (Parameters.q)
            {
                case <= 7.1:
                    Parameters.q = 7.1;
                    break;

                case <= 8:
                    Parameters.q = 8;
                    break;

                case <= 9:
                    Parameters.q = 9;
                    break;

                case <= 10:
                    Parameters.q = 10;
                    break;

                case <= 11.2:
                    Parameters.q = 11.2;
                    break;

                case <= 12.5:
                    Parameters.q = 12.5;
                    break;

                case <= 14:
                    Parameters.q = 14;
                    break;

                case <= 16:
                    Parameters.q = 16;
                    break;

                case <= 18:
                    Parameters.q = 18;
                    break;

                case <= 20:
                    Parameters.q = 20;
                    break;

                case <= 22.4:
                    Parameters.q = 22.4;
                    break;

                case <= 25:
                    Parameters.q = 25;
                    break;

                default:
                    MessageBox.Show("Невходящее значение q");
                    break;
            }
            output_text.Text += $"Окончательное значение q принимаем равным {Parameters.q}" + Environment.NewLine;

            //MessageBox.Show("Имеется ли у вас смещение Х (несовпадение делительного диаметра с начальным) 1- Да, 2 - Нет");
            switch (X_tetxt.Text)
            {
                case "1":
                    Parameters.X = ((Parameters.Aw - (0.5 * Parameters.m * (Parameters.z2 + Parameters.q))) /
                                          Parameters.m);
                    if (Parameters.X >= -1 && Parameters.X <= 1)
                    {
                        output_text.Text += $"Коэффициент смещения Х допустим и равен {Parameters.X}" + Environment.NewLine;
                    }
                    else
                    {
                        while ((Parameters.X <= -1 || Parameters.X >= 1))
                        {
                            MessageBox.Show(
                                "Рассчитанный параметр Х не входит в допустимый диапазон. Необходимо варьировать значения Aw, m, q");

                            MessageBox.Show("Введите новое значение Aw:...");
                            Parameters.Aw = Int32.Parse(Console.ReadLine());

                            MessageBox.Show("Введите новое значение m:...");
                            Parameters.m = Int32.Parse(Console.ReadLine());

                            MessageBox.Show("Введите новое значение q:...");
                            Parameters.q = Int32.Parse(Console.ReadLine());

                            MessageBox.Show("Рассчитаем X заново...");
                            Parameters.X = ((Parameters.Aw - (0.5 * Parameters.m * (Parameters.z2 + Parameters.q))) /
                                            Parameters.m);
                        }
                        output_text.Text += $"Обновленный параметр Х равен {Parameters.X}" + Environment.NewLine; // найти способ зациклить
                    }
                    Parameters.Type = 1;
                    break;

                case "2":
                    Parameters.X = 0;
                    //MessageBox.Show("Рассчитываем далее...");
                    Parameters.Type = 2;
                    break;
            }

            //MessageBox.Show("Рассчитаем угол подъема винтовой линии на начальном диаметре");
            if (Parameters.Type == 1)
            {
                Parameters.angleGammaRadians = Math.Atan(Parameters.z1 / (Parameters.q + 2 * Parameters.X));
                Parameters.angleGamma = Parameters.angleGammaRadians * (180 / Math.PI);

                output_text.Text += $"Угол подъема винтовой линии на начальном диаметре равен {Parameters.angleGammaRadians} (рад) или {Parameters.angleGamma} (град)" + Environment.NewLine;

            }
            else if (Parameters.Type == 2)
            {
                Parameters.angleGammaRadians = Math.Atan(Parameters.z1 / (Parameters.q + 2 * Parameters.X));
                Parameters.angleGamma = Parameters.angleGammaRadians * (180 / Math.PI);

                output_text.Text += $"Угол подъема винтовой линии на начальном диаметре равен {Parameters.angleGammaRadians} (рад) или {Parameters.angleGamma} (град)" + Environment.NewLine;
            }

            //MessageBox.Show("Введем коэффициент ширины венцa Yba (0,2 - 0,63)");
            Parameters.Yba = Double.Parse(Yba_text.Text);
            try
            {
                if (Parameters.Yba >= 0.2 && Parameters.Yba <= 0.63)
                {
                    
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

            //MessageBox.Show("Определим коэффициент ширины зубчатого венца Ybd:");
            Parameters.Ybd = 0.5 * Parameters.Yba * (Parameters.PerPr1 + 1);
            output_text.Text += $"Значение Ybd равно {Parameters.Ybd}" + Environment.NewLine;

            //MessageBox.Show("Выберите ваше значение твердости материала зубчатого колеса. Нажмите: 1 - если тведость менее 350HB, 2 - если твердость более 350HB");

            switch (solid_text.Text)
            {
                case "1":
                    switch (Parameters.Ybd)
                    {
                        case <= 0.2:
                            Parameters.Khbeta = 1.01;
                            break;

                        case <= 0.4:
                            Parameters.Khbeta = 1.02;
                            break;

                        case <= 0.6:
                            Parameters.Khbeta = 1.025;
                            break;

                        case <= 0.8:
                            Parameters.Khbeta = 1.027;
                            break;

                        case <= 1.0:
                            Parameters.Khbeta = 1.04;
                            break;

                        case <= 1.2:
                            Parameters.Khbeta = 1.05;
                            break;

                        case >= 1.2:
                            Parameters.Khbeta = 1.05;
                            break;
                    }
                    output_text.Text += $"При вычисленном значении Ybd, значение Khbeta = {Parameters.Khbeta}" + Environment.NewLine;
                    break;

                case "2":
                    switch (Parameters.Ybd)
                    {
                        case <= 0.2:
                            Parameters.Khbeta = 1.01;
                            break;

                        case <= 0.4:
                            Parameters.Khbeta = 1.02;
                            break;

                        case <= 0.6:
                            Parameters.Khbeta = 1.0375;
                            break;

                        case <= 0.8:
                            Parameters.Khbeta = 1.05;
                            break;

                        case <= 1.0:
                            Parameters.Khbeta = 1.09;
                            break;

                        case <= 1.2:
                            Parameters.Khbeta = 1.12;
                            break;
                        case >= 1.2:
                            Parameters.Khbeta = 1.12;
                            break;
                    }
                    output_text.Text += $"При вычисленном значении Ybd, значение Khbeta = {Parameters.Khbeta}" + Environment.NewLine;
                    break;
            }

            //MessageBox.Show("Рассчитаем делительный диаметр червяка d1...");
            Parameters.d1 = Convert.ToInt32(Parameters.m * Parameters.q);
            output_text.Text += $"Делительный диаметр червяка d1 равен {Parameters.d1}" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем делительный диаметр червячного колеса d2...");
            Parameters.d2 = Convert.ToInt32(Parameters.m * Parameters.z2);
            output_text.Text += $"Делительный диаметр червячного колеса d2 равен {Parameters.d2}" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем начальный диаметр червяка...");
            Parameters.dw1 = Parameters.m * (Parameters.q + 2 * Parameters.X);
            output_text.Text += $"Начальный диаметр червяка равен {Parameters.dw1}" + Environment.NewLine;
        }

        public void TensionsCheck()
        {
            //MessageBox.Show("Найдем действующее напряжение в контакте витка червяка и зуба колеса:...");
            Parameters.sigma_n = (170 * Parameters.q / Parameters.z2) * Math.Sqrt(Parameters.T3 * 1000 * 1.2 * Math.Pow(((Parameters.z2 / Parameters.q) + 1), 3) / Math.Pow(Parameters.Aw, 3));
            output_text.Text += $"Действующее напряжение в контакте витка червяка и зуба колеса равно {Parameters.sigma_n}" + Environment.NewLine;
            try
            {
                if (Parameters.sigma_n < Parameters.SIGMA_Ha2)
                {
                    Parameters.percentage = (Parameters.sigma_n * 100) / Parameters.SIGMA_Ha2;
                    output_text.Text += $"Условие прочности по контактным напряжениям выполнено. Процент недонапряжения = {Parameters.percentage}" + Environment.NewLine;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Условие прочности по контактным напряжениям не выполнено!");
                MessageBox.Show(ex.Message);
                throw;
            }

            //MessageBox.Show("Рассчитаем приведенное число зубьев z2v...");
            Parameters.z2v = Math.Round(Parameters.z2 / Math.Pow(Math.Cos(Parameters.angleGammaRadians), 3), MidpointRounding.AwayFromZero); // способ математического округления
            output_text.Text += $"Приведенное число зубьев z2v = {Parameters.z2v}" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем окружную силу на колесе Ft2...");
            Parameters.Ft2 = (2 * Parameters.T2 * 1000) / Parameters.d2;
            output_text.Text += $"Окружную сила на колесе Ft2 = {Parameters.Ft2}" + Environment.NewLine;

            //MessageBox.Show("Проведем выбор коэффициента формы зуба червячного колеса Yf...");
            switch (Parameters.z2v)
            {
                case <= 20:
                    Parameters.Yf = 1.98;
                    break;

                case <= 24:
                    Parameters.Yf = 1.88;
                    break;

                case <= 26:
                    Parameters.Yf = 1.85;
                    break;

                case <= 28:
                    Parameters.Yf = 1.80;
                    break;

                case <= 30:
                    Parameters.Yf = 1.76;
                    break;

                case <= 32:
                    Parameters.Yf = 1.71;
                    break;

                case <= 35:
                    Parameters.Yf = 1.64;
                    break;

                case <= 37:
                    Parameters.Yf = 1.61;
                    break;

                case <= 40:
                    Parameters.Yf = 1.55;
                    break;

                case <= 45:
                    Parameters.Yf = 1.48;
                    break;

                case <= 50:
                    Parameters.Yf = 1.45;
                    break;

                case <= 60:
                    Parameters.Yf = 1.40;
                    break;

                case <= 80:
                    Parameters.Yf = 1.34;
                    break;

                case <= 100:
                    Parameters.Yf = 1.30;
                    break;

                case <= 150:
                    Parameters.Yf = 1.27;
                    break;

                case <= 300:
                    Parameters.Yf = 1.24;
                    break;

            }
            output_text.Text += $"Yf = {Parameters.Yf}" + Environment.NewLine;
        }

        public void Warmth()
        {
            //MessageBox.Show("Рассчитаем окружную скорость V...");
            Parameters.V = (Math.PI * Parameters.d1 * Parameters.n1) / (60 * 1000);
            output_text.Text += $"Окружная скорость равна {Parameters.V} м/с" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем скорость скольжения Vs...");
            Parameters.VS = Parameters.V / Math.Cos(Parameters.angleGammaRadians);
            output_text.Text += $"Скорость скольжения Vs = {Parameters.VS} м/с" + Environment.NewLine;

            //MessageBox.Show("Проведем выбор приведенных коэффициентов трения f и углов трения ro между витками червяка и зубьями колеса...");
            switch (Parameters.VS)
            {
                case <= 0.01:
                    Parameters.f = 0.11;
                    Parameters.ro = 6;
                    break;

                case <= 0.1:
                    Parameters.f = 0.085;
                    Parameters.ro = 4.83;
                    break;

                case <= 0.25:
                    Parameters.f = 0.07;
                    Parameters.ro = 4;
                    break;

                case <= 0.5:
                    Parameters.f = 0.07;
                    Parameters.ro = 3.33;
                    break;

                case <= 1.0:
                    Parameters.f = 0.05;
                    Parameters.ro = 2.83;
                    break;

                case <= 1.5:
                    Parameters.f = 0.045;
                    Parameters.ro = 2.583;
                    break;

                case <= 2.0:
                    Parameters.f = 0.04;
                    Parameters.ro = 2.25;
                    break;

                case <= 2.5:
                    Parameters.f = 0.035;
                    Parameters.ro = 2;
                    break;

                case <= 3.0:
                    Parameters.f = 0.0325;
                    Parameters.ro = 1.75;
                    break;

                case <= 4.0:
                    Parameters.f = 0.0265;
                    Parameters.ro = 1.5;
                    break;

                case <= 7.0:
                    Parameters.f = 0.022;
                    Parameters.ro = 1.25;
                    break;

                case <= 10.0:
                    Parameters.f = 0.02;
                    Parameters.ro = 1;
                    break;

                case <= 15.0:
                    Parameters.f = 0.017;
                    Parameters.ro = 1;
                    break;
            }
            output_text.Text += $"Приведенный коэф. трения f = {Parameters.f}, угол трения ro = {Parameters.ro} град." + Environment.NewLine;

            //MessageBox.Show("Рассчитаем КПД червячной передачи...");
            Parameters.KPDcherv = Math.Tan(Parameters.angleGamma * (Math.PI / 180)) / (Math.Tan(Parameters.angleGamma * (Math.PI / 180)) + Math.Tan(Parameters.ro * (Math.PI / 180)));
           
            output_text.Text += $"КПД червячной передачи равно {Parameters.KPDcherv}" + Environment.NewLine;

            //MessageBox.Show("Вычислим требуемую мощность на входе в редуктор P1...");
            Parameters.P2 = (Parameters.T3 * Parameters.LeadingChervVal * 1000) / 9550;
            output_text.Text += $"Мощность на выходе P2 = {Parameters.P2}" + Environment.NewLine;

            Parameters.P1 = Parameters.P2 / Parameters.KPDcherv;
            output_text.Text += $"Требуемая мощность на входе P1 = {Parameters.P1}" + Environment.NewLine;

            //MessageBox.Show("Введите примерную площадь поверхности теплоизлучения редуктора А");
            Parameters.A = double.Parse(A_text.Text);

            //MessageBox.Show("Рассчитаем температуру масла в редукторе в отсутствие вентилятора и при Kt = 12:");
            Parameters.t = ((((1 - Parameters.KPDcherv) * Parameters.P1) / (12 * Parameters.A))) + Parameters.outsideTemp;
            if (Parameters.t > 95)
            {
                output_text.Text += "Необходимо установки вентилятора в редуктор, либо увеличение площади поверхности корпуса" + Environment.NewLine;
            }
            else
            {
                output_text.Text += "Нет необходимости в установке вентилятора или в увеличении площади поверхности корпуса" + Environment.NewLine;
            }
        }

        public void GeometrySizes()
        {
            //MessageBox.Show("Рассчитаем диаметр витков червяка da1:...");
            Parameters.da1 = Convert.ToInt32(Parameters.d1 + 2*(Parameters.ha * Parameters.m));
            output_text.Text += $"Диаметр витков червяка da1 = {Parameters.da1} мм" + Environment.NewLine;

            //MessageBox.Show("Найдем диаметр вершин зубьев червячного колеса da2:...");
            Parameters.da2 = Convert.ToInt32(Parameters.d2 + (2 * (Parameters.ha + Parameters.X) * Parameters.m));
            output_text.Text += $"Диаметр вершин зубьев червячного колеса da2 = {Parameters.da2}" + Environment.NewLine;

            //MessageBox.Show("Вычислим наибольший диаметр червячного колеса daM2:...");
            Parameters.daM2 = Convert.ToInt32(Parameters.da2 + (6 * Parameters.m / (Parameters.z1 + 2)));
            output_text.Text += $"Наибольший диаметр червячного колеса daM2 = {Parameters.daM2}" + Environment.NewLine;

            if (Parameters.z1 == 1 || Parameters.z1 == 2)
            {
                switch (Parameters.X)
                {
                    case <= -1.0:
                        Parameters.b1 = Math.Round(((10.5 + 0.06 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= -0.5:
                        Parameters.b1 = Math.Round(((8 + 0.06 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 0:
                        Parameters.b1 = Math.Round(((11 + 0.06 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 0.5:
                        Parameters.b1 = Math.Round(((11 + 0.1 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 1.0:
                        Parameters.b1 = Math.Round(((12 + 0.1 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;
                }
            }
            else if (Parameters.z1 == 3 || Parameters.z1 == 4)
            {
                switch (Parameters.X)
                {
                    case <= -1.0:
                        Parameters.b1 = Math.Round(((10.5 + 0.09 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= -0.5:
                        Parameters.b1 = Math.Round(((9.5 + 0.09 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 0:
                        Parameters.b1 = Math.Round(((12.5 + 0.09 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 0.5:
                        Parameters.b1 = Math.Round(((12.5 + 0.1 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;

                    case <= 1.0:
                        Parameters.b1 = Math.Round(((13 + 0.1 * Parameters.z2) * Parameters.m), MidpointRounding.AwayFromZero);
                        break;
                }
            }
            output_text.Text += $"При заданных значениях Х = {Parameters.X} и z1 = {Parameters.z1}, длина нарезанной части червяка b1 = {Parameters.b1} мм" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем ширину венца червячного колеса b2:...");
            output_text.Text += "!!!!Угол охвата delta = 0.75 рад. как константное значение!!!!" + Environment.NewLine;
            Parameters.b2 = Math.Round((0.75 * Parameters.da1), MidpointRounding.AwayFromZero);
            output_text.Text += $"Ширина венца червячного колеса b2 = {Parameters.b2}" + Environment.NewLine;


            //MessageBox.Show("Вычислим допустимое напряжение изгиба колеса Sigma_Fa... (данный расчет относится к условию прочности по контактным и изгибным напряжениям)");
            Parameters.Sigma_Fa = (1.2 * Parameters.T3 * 1.5 * Parameters.Yf) /
                                 (Parameters.z2 * Math.Pow(Parameters.m, 2) * Parameters.b2);
            output_text.Text += $"Допустимое напряжение изгиба колеса Sigma_Fa = {Parameters.Sigma_Fa}" + Environment.NewLine;

            if (Parameters.Sigma_Fa < Parameters.SIGMA_F || Parameters.Sigma_Fa < Parameters.SIGMA_F1)
            {
                output_text.Text += "Допустимое напряжение изгиба удовлетворяет условию" + Environment.NewLine;
                Powers();
            }
            else
            {
                MessageBox.Show("Допустимое напряжение изгиба не удовлетворяет условию");
            }
        }

        public void Powers()
        {
            //MessageBox.Show("Рассчитаем окружную силу на колесе Ft2 и осевую силу на червяке Fa1 (Ft2 = Fa1):...");
            Parameters.Fa1 = (2 * Parameters.T2) / Parameters.d2;
            output_text.Text += $"Окружная сила на колесе Ft2 и осевая сила на червяке Fa1 = {Parameters.Fa1} Н" + Environment.NewLine;

            //MessageBox.Show("Рассчитаем окружную силу на червяке Ft1 и осевую силу на колесе Fa2 (Ft1 = Fa2):...");
            Parameters.Ft1 = (2 * Parameters.T3) / Parameters.d1;
            output_text.Text += $"Окружная сила на червяке Ft1 и осевая сила на колесе Fa2 = {Parameters.Ft1} Н" + Environment.NewLine;

            //MessageBox.Show("Радиальная сила в зацеплении Fr:...");
            Parameters.Fr = (Parameters.Fa1 * Math.Tan(Parameters.alpha) / Math.Cos(Parameters.angleGammaRadians));
            output_text.Text += $"Радиальная сила в зацеплении Fr = {Parameters.Fr} Н" + Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Beginning();
            KPDCounting();

            if (Parameters.KPD < 1)
            {
                edPower();
            }
            else
            {
                KPDCounting();
            }

            AllNumber();
            KinematicCounter();
            ZubchatieWheels();
            AllowedTensions();
            InterAxial();
            Module();
            TensionsCheck();
            Warmth();
            GeometrySizes();
            Powers();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           Beginning();
           KPDCounting();
            if (Parameters.KPD > 1)
            {
                return;
            }
            else
            {
                edPower();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AllNumber();
            if (Parameters.difference > Parameters.PerPr1 * 0.1)
            {
                return;
            }
            else
                KinematicCounter();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZubchatieWheels();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AllowedTensions();
            if (Parameters.SIGMA_Ha2 <= (Parameters.SIGMA_F * 0.55) && Parameters.SIGMA_Ha2 >= (0.95 * Parameters.SIGMA_F) || Parameters.SIGMA_Ha2 <= (Parameters.SIGMA_F1 * 0.55) && Parameters.SIGMA_Ha2 >= (0.95 * Parameters.SIGMA_F1))
            {
                return;
            }
            else
            {
                InterAxial();
            }

            Module();
            if (Parameters.Yba <= 0.2 && Parameters.Yba >= 0.63)
            {
                return;
            }
            else
            {
                TensionsCheck();
            }
           
            Warmth();

            GeometrySizes();
            if (Parameters.Sigma_Fa > Parameters.SIGMA_F || Parameters.Sigma_Fa > Parameters.SIGMA_F1)
            {
                return;
            }
            else
            {
                Powers();
            }
        }
    }
}
