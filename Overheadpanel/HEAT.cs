﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSInterface;
using FSToolbox;

namespace Overheadpanel
{
    class HEAT : Panel
    {
        public HEAT()
        {
            //debug variable
            is_debug = true;

            //starting FSI Client for IRS
            FSIcm.inst.OnVarReceiveEvent += fsiOnVarReceive;
            FSIcm.inst.DeclareAsWanted(new FSIID[]
                {
                    FSIID.MBI_HEAT_OVHT_TEST_SWITCH,
                    FSIID.MBI_HEAT_PROBE_HEAT_A_SWITCH,
                    FSIID.MBI_HEAT_PROBE_HEAT_B_SWITCH,
                    FSIID.MBI_HEAT_PWR_TEST_SWITCH,
                    FSIID.MBI_HEAT_WINDOW_LEFT_FWD_SWITCH,
                    FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_SWITCH,
                    FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_SWITCH,
                    FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_SWITCH
                }
            );

            //standard values
            LightController.set(FSIID.MBI_HEAT_AUX_PITOT_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_CAPT_PITOT_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_FO_PITOT_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_L_ALPHA_VANE_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_L_ELEV_PITOT_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_R_ALPHA_VANE_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_R_ELEV_PITOT_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_TEMP_PROBE_LIGHT, true);
            LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_FWD_ON_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_FWD_OVERHEAT_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_ON_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_OVERHEAT_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_ON_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_OVERHEAT_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_ON_LIGHT, false);
            LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_OVERHEAT_LIGHT, false);

            FSIcm.inst.MBI_HEAT_LAMPTEST = false;

            FSIcm.inst.ProcessWrites();
        }


        void fsiOnVarReceive(FSIID id)
        {
            //OVERHEAT TEST ON
            if (id == FSIID.MBI_HEAT_OVHT_TEST_SWITCH && FSIcm.inst.MBI_HEAT_OVHT_TEST_SWITCH == true)
            {
                debug("HEAT OVHT TEST On");

                //OVHT tst lights
                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_FWD_OVERHEAT_LIGHT, true);
                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_OVERHEAT_LIGHT, true);
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_OVERHEAT_LIGHT, true);
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_OVERHEAT_LIGHT, true);
                LightController.ProcessWrites();
            }

            //OVERHEAT TEST OFF
            if (id == FSIID.MBI_HEAT_OVHT_TEST_SWITCH && FSIcm.inst.MBI_HEAT_OVHT_TEST_SWITCH == false)
            {
                debug("HEAT OVHT TEST Off");

                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_FWD_OVERHEAT_LIGHT, false);
                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_OVERHEAT_LIGHT, false);
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_OVERHEAT_LIGHT, false);
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_OVERHEAT_LIGHT, false);
                LightController.ProcessWrites();
            }

            //WND LEFT FWD
            if (id == FSIID.MBI_HEAT_WINDOW_LEFT_FWD_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_WINDOW_LEFT_FWD_SWITCH)
                {
                    debug("HEAT WND L FWD On");
                }
                else
                {
                    debug("HEAT WND L FWD Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_FWD_ON_LIGHT, FSIcm.inst.MBI_HEAT_WINDOW_LEFT_FWD_SWITCH);
                LightController.ProcessWrites();
            }


            //WND LEFT SIDE
            if (id == FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_WINDOW_LEFT_SIDE_SWITCH)
                {
                    debug("HEAT WND L SIDE On");
                }
                else
                {
                    debug("HEAT WND L SIDE Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_WINDOW_LEFT_SIDE_ON_LIGHT, FSIcm.inst.MBI_HEAT_WINDOW_LEFT_SIDE_SWITCH);
                LightController.ProcessWrites();
            }

            //WND RIGHT FWD
            if (id == FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_WINDOW_RIGHT_FWD_SWITCH)
                {
                    debug("HEAT WND R FWD On");
                }
                else
                {
                    debug("HEAT WND R FWD Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_FWD_ON_LIGHT, FSIcm.inst.MBI_HEAT_WINDOW_RIGHT_FWD_SWITCH);
                LightController.ProcessWrites();
            }


            //WND RIGHT SIDE
            if (id == FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_WINDOW_RIGHT_SIDE_SWITCH)
                {
                    debug("HEAT WND R SIDE On");
                }
                else
                {
                    debug("HEAT WND R SIDE Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_WINDOW_RIGHT_SIDE_ON_LIGHT, FSIcm.inst.MBI_HEAT_WINDOW_RIGHT_SIDE_SWITCH);
                LightController.ProcessWrites();

            }


            //Probes A
            if (id == FSIID.MBI_HEAT_PROBE_HEAT_A_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_PROBE_HEAT_A_SWITCH)
                {
                    debug("HEAT PROBE A On");
                }
                else
                {
                    debug("HEAT PROBE A Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_L_ALPHA_VANE_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_A_SWITCH);
                LightController.set(FSIID.MBI_HEAT_L_ELEV_PITOT_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_A_SWITCH);
                LightController.set(FSIID.MBI_HEAT_CAPT_PITOT_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_A_SWITCH);
                LightController.set(FSIID.MBI_HEAT_TEMP_PROBE_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_A_SWITCH);
                LightController.ProcessWrites();

            }

            //Probes B
            if (id == FSIID.MBI_HEAT_PROBE_HEAT_B_SWITCH)
            {
                if (FSIcm.inst.MBI_HEAT_PROBE_HEAT_B_SWITCH)
                {
                    debug("HEAT PROBE B On");
                }
                else
                {
                    debug("HEAT PROBE B Off");
                }

                //set lights
                LightController.set(FSIID.MBI_HEAT_R_ALPHA_VANE_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_B_SWITCH);
                LightController.set(FSIID.MBI_HEAT_R_ELEV_PITOT_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_B_SWITCH);
                LightController.set(FSIID.MBI_HEAT_FO_PITOT_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_B_SWITCH);
                LightController.set(FSIID.MBI_HEAT_AUX_PITOT_LIGHT, !FSIcm.inst.MBI_HEAT_PROBE_HEAT_B_SWITCH);
                LightController.ProcessWrites();

            }
        }
    }
}
