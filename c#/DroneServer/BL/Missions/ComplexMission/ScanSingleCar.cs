﻿using DroneServer.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServer.BL.Missions
{
    public class ScanSingleCar : ComplexMission
    {
        private Point m_curr_position;
        private int m_go_to_car_index;
        private int m_get_to_certain_height;
        private int m_move_back_index;
        private int m_get_plate;

        private Car m_car;

        public ScanSingleCar(Point curr_position, Car car, ComplexMission ParentMission = null) : base(ParentMission)
        {
            m_curr_position = curr_position;
            m_car = car;

            GoToCar go_to_car = new GoToCar(m_curr_position, m_car, this);
            m_go_to_car_index = go_to_car.m_index;
            m_SubMission.Enqueue(go_to_car);
        }

        public override void stop()
        {

        }

        public override void notify(Response response)
        {
            if (response.Key == m_go_to_car_index)
            {
                Mission move_back = new MoveMission(this, Direction.backward, calc_amount_to_move_backward());
                m_move_back_index = move_back.m_index;
                move_back.execute();

            }
            else if (response.Key == m_move_back_index)
            { 
                double height = Double.Parse(Configuration.getInstance().get("height_of_drone_when_get_close_to_car"));
                Mission m = new GetToCertainHeight(height, this);
                m_get_to_certain_height = m.m_index;
                m.execute();
            }
            else if (response.Key == m_get_to_certain_height)
            {
                GetCarPlate get_plate = new GetCarPlate(m_car, this);
                m_get_plate = get_plate.m_index;
                get_plate.execute();
            }
            else if (response.Key == m_get_plate)
            {
                VerifyLocation get_location = new VerifyLocation(this);
                get_location.execute();
            }
            else
            {
                Logger.getInstance().info("finish scan single car");
                BL.BLManagger.getInstance().num_of_scaned_cars++;
                done(new Response(m_index, Status.Ok, MissionType.MainMission, response.Data));
            }
        }

        private double calc_amount_to_move_backward()
        {
            double car_height = m_car.getCarHeight();
            double amount_to_move_backward_of_car = Double.Parse(Configuration.getInstance().get("amount_to_move_backward_of_car"));

            return car_height + amount_to_move_backward_of_car;
        }

    }
}
