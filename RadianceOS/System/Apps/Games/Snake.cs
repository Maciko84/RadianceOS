﻿using CosmosTTF;
using Microsoft.VisualBasic;
using RadianceOS.System.Graphic;
using RadianceOS.System.Managment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadianceOS.System.Apps.Games
{
	public static class Snake
	{
		public static void Start(int ProcessID)
		{
			Process.Processes[ProcessID].fragments = new List<fragment>();
			Process.Processes[ProcessID].fragments2 = new List<fragment>();
			fragment frag = new fragment
			{
				x = 16,
				y = 15,
				lastx = 15,
				 lasty = 14
			};
			fragment frag2 = new fragment
			{
				x = 15,
				y = 15,
				lastx = 14,
				lasty = 14
			};
			Process.Processes[ProcessID].fragments.Add(frag);
			Process.Processes[ProcessID].fragments.Add(frag2);
		



			for (int j = 0; j < Process.Processes.Count; j++)
			{
				Process.Processes[j].selected = false;
			}
			Process.Processes[ProcessID].tempInt = 1;
			Process.Processes[ProcessID].selected = true;
			int best = 0;
			if(Kernel.diskReady)
			{
				if (Directory.Exists(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake"))
				{
					if (File.Exists(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat"))
					{
						best = int.Parse(File.ReadAllText(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat"));
					}
					else
					{
						File.Create(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat");
						File.WriteAllText(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat", "0");
					}
				}
				else
				{
					Directory.CreateDirectory(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake");
					File.Create(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat");
					File.WriteAllText(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat", "0");
				}
			}
			else
			{
				MessageBoxCreator.CreateMessageBox("Warning", "Unable to load Best Score.\nRadianceOS is not installed on this computer.", MessageBoxCreator.MessageBoxIcon.warning, 500);
			}
			
			Process.Processes[ProcessID].tempList = new List<int> { 40, 6, 0, 2 , best};


			Kernel.countFPS = true;
		}


		public static void Render(int X, int Y, int SizeX, int SizeY, int i, List<fragment> fragments, List<fragment> fragments2)
		{
			if (fragments == null)
				return;
			Window.DrawTop(i, X, Y, SizeX, "Snake");
			Explorer.CanvasMain.DrawFilledRectangle(Kernel.shadow, X + 3, Y + 28, SizeX, SizeY - 25);
			Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X, Y+25, SizeX, SizeY-25);
			
			Explorer.CanvasMain.DrawFilledRectangle(Kernel.middark, X + 2, Y + 27 + 21, SizeX - 4, SizeY - 50);

		
		switch(Process.Processes[i].tempList[2])
			{
				case 0:
					{
						StringsAcitons.DrawCenteredString("Game settings", SizeX, X, Y + 29, 15, Color.White, Kernel.fontRuscii);
						StringsAcitons.DrawCenteredString("Speed: "+ Process.Processes[i].tempList[1], SizeX, X, Y + 379, 15, Color.White, Kernel.fontRuscii);
						StringsAcitons.DrawCenteredString("Best: " + Process.Processes[i].tempList[4], SizeX, X, Y + 79, 15, Color.White, Kernel.fontRuscii);
						//SPEED
						if (Explorer.MY > Y + 379 - 17 && Explorer.MY < Y + 379 + 33)
						{
							if(Explorer.MX > X + 350 && Explorer.MX < X + 400)
							{
							
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 379 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 400, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + 350, Y + 379 - 17, 50, 50);
								if (Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
								{
									if(!Explorer.Clicked && Process.Processes[i].tempList[1] > 1)
									Process.Processes[i].tempList[1]--;
									StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 403, 18, Color.White, "UMB", 50);
								}
								else
								{
									StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 401, 18, Color.White, "UMB", 50);
								}
							}
							else if(Explorer.MX > X + 500 & Explorer.MX < X + 550)
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 379 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 400, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + 500, Y + 379 - 17, 50, 50);

								if (Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
								{
									if (!Explorer.Clicked && Process.Processes[i].tempList[1] < 20)
										Process.Processes[i].tempList[1]++;
									StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 403, 18, Color.White, "UMB", 50);
								}
								else
								{
									StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 401, 18, Color.White, "UMB", 50);
								}

							
							}
							else
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 379 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 400, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 379 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 400, 18, Color.White, "UMB", 50);
							}
						}
						else
						{
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 379 - 17, 50, 50);
							StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 400, 18, Color.White, "UMB", 50);
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 379 - 17, 50, 50);
							StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 400, 18, Color.White, "UMB", 50);
						}

						//APPLES
						if (Explorer.MY > Y + 479 - 17 && Explorer.MY < Y + 479 + 33)
						{
							if (Explorer.MX > X + 350 && Explorer.MX < X + 400)
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 479 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 500, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + 350, Y + 479 - 17, 50, 50);
								if (Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
								{
									if (!Explorer.Clicked && Process.Processes[i].tempList[3] > 1)
										Process.Processes[i].tempList[3]--;
									StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 503, 18, Color.White, "UMB", 50);
								}
								else
								{
									StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 501, 18, Color.White, "UMB", 50);
								}

							}
							else if (Explorer.MX > X + 500 & Explorer.MX < X + 550)
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 479 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 500, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + 500, Y + 479 - 17, 50, 50);

								if (Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
								{
									if (!Explorer.Clicked && Process.Processes[i].tempList[3] < 6)
										Process.Processes[i].tempList[3]++;
									StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 503, 18, Color.White, "UMB", 50);
								}
								else
								{
									StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 501, 18, Color.White, "UMB", 50);
								}


							}
							else
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 479 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 500, 18, Color.White, "UMB", 50);
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 479 - 17, 50, 50);
								StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 500, 18, Color.White, "UMB", 50);
							}

						}
						else
						{
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 350, Y + 479 - 17, 50, 50);
							StringsAcitons.DrawCenteredTTFString("-", 50, X + 356, Y + 500, 18, Color.White, "UMB", 50);
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 500, Y + 479 - 17, 50, 50);
							StringsAcitons.DrawCenteredTTFString("+", 50, X + 503, Y + 500, 18, Color.White, "UMB", 50);
						}
						StringsAcitons.DrawCenteredString("Apples: " + Process.Processes[i].tempList[3], SizeX, X, Y + 479, 15, Color.White, Kernel.fontRuscii);



						//START BUTTON
						if(Explorer.MY > Y + 600 && Explorer.MY < Y + 650)
						{
							if(Explorer.MX > X + 400 && Explorer.MX < X + 500)
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + 400, Y + 600, 100, 50);
								if (Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
								{
									if (!Explorer.Clicked)
									{
										Random random = new Random();
										for (int o = 0; o < Process.Processes[i].tempList[3]; o++)
										{
											int x1 = random.Next(0, 30);
											int y1 = random.Next(2, 31);
											fragment fragApple = new fragment
											{
												x = x1,
												y = y1
											};
											Process.Processes[i].fragments2.Add(fragApple);
										}
										for (int o = 0; o < Process.Processes[i].tempList[3]; o++)
										{
											for (int j = 0; j < Process.Processes[i].tempList[3]; j++)
											{
												if (o != j && Process.Processes[i].fragments2[o].x == Process.Processes[i].fragments2[j].x)
												{
													Process.Processes[i].fragments2[i].x = random.Next(0, 30);
												}

												if (o != j && Process.Processes[i].fragments2[o].y == Process.Processes[i].fragments2[j].y)
												{
													Process.Processes[i].fragments2[i].y = random.Next(2, 31);
												}
											}
										}
										Process.Processes[i].tempList[2] = 1;
									}
									
									StringsAcitons.DrawCenteredString("START", 100, X + 400, Y + 622, 15, Color.White, Kernel.fontRuscii);
								}
								else
								{
									StringsAcitons.DrawCenteredString("START", 100, X + 400, Y + 620, 15, Color.White, Kernel.fontRuscii);
								}
							}
							else
							{
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 400, Y + 600, 100, 50);
								StringsAcitons.DrawCenteredString("START", 100, X + 400, Y + 619, 15, Color.White, Kernel.fontRuscii);
							}
						}
						else
						{
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.main, X + 400, Y + 600, 100, 50);
							StringsAcitons.DrawCenteredString("START", 100, X + 400, Y + 619, 15, Color.White, Kernel.fontRuscii);
						}
					}
					break;
				case 1:
					{
					# region snake
						if (Process.Processes[i].selected && !Process.Processes[i].tempBool && Kernel._fps > Process.Processes[i].tempList[1])
						{



							StringsAcitons.DrawCenteredString("Update every " + Process.Processes[i].tempList[0] + " frames (" + Process.Processes[i].tempList[1] + "/s)", SizeX, X, Y + 29, 15, Color.White, Kernel.fontRuscii);
							bool update = false;
							int dirrection = Process.Processes[i].tempInt3;



							while (Console.KeyAvailable)
							{
								ConsoleKeyInfo key = Console.ReadKey(true);
								switch (key.Key)
								{
									case ConsoleKey.DownArrow:
										{
											if (dirrection != 3)
											{
												Process.Processes[i].tempInt3 = 1;
											}
										}
										break;
									case ConsoleKey.UpArrow:
										{
											if (dirrection != 1)
											{
												Process.Processes[i].tempInt3 = 3;
											}
										}
										break;
									case ConsoleKey.RightArrow:
										{
											if (dirrection != 2)
											{
												Process.Processes[i].tempInt3 = 0;
											}
										}
										break;
									case ConsoleKey.LeftArrow:
										{
											if (dirrection != 0)
											{
												Process.Processes[i].tempInt3 = 2;
											}
										}
										break;
									case ConsoleKey.S:
										{
											if (dirrection != 3)
											{
												Process.Processes[i].tempInt3 = 1;
											}
										}
										break;
									case ConsoleKey.W:
										{
											if (dirrection != 1)
											{
												Process.Processes[i].tempInt3 = 3;
											}
										}
										break;
									case ConsoleKey.D:
										{
											if (dirrection != 2)
											{
												Process.Processes[i].tempInt3 = 0;
											}
										}
										break;
									case ConsoleKey.A:
										{
											if (dirrection != 0)
											{
												Process.Processes[i].tempInt3 = 2;
											}
										}
										break;
								}

							}

							if (Process.Processes[i].tempInt2 > Process.Processes[i].tempList[0]/*curr speed*/) //pls cosmos add milliseconds support!@!
							{
								if (Kernel._fps != 0)
								{
									Process.Processes[i].tempList[0] = Kernel._fps / Process.Processes[i].tempList[1];
								}
								fragments[0].lasty = fragments[0].y;
								fragments[0].lastx = fragments[0].x;

								switch (dirrection)
								{
									case 0:
										fragments[0].x++;
										break;
									case 2:
										fragments[0].x--;
										break;
									case 3:
										fragments[0].y--;
										break;
									case 1:
										fragments[0].y++;
										break;
								}
								update = true;
								Process.Processes[i].tempInt2 = 0;
								for (int j = 0; j < fragments2.Count; j++)
								{

									if (fragments[0].x == fragments2[j].x && fragments[0].y == fragments2[j].y)
									{
										Process.Processes[i].tempInt++;
										Random random = new Random();

										int x1 = random.Next(0, 30);
										int y1 = random.Next(2, 31);
										while (x1 == fragments2[j].x)
										{
											x1 = random.Next(0, 30);
										}
										while (y1 == fragments2[j].y)
										{
											y1 = random.Next(2, 31);
										}
										fragments2[j].x = x1;
										fragments2[j].y = y1;
										int count = Process.Processes[i].fragments.Count;
										fragment newFrag = new fragment
										{
											x = Process.Processes[i].fragments[count - 1].lastx,
											y = Process.Processes[i].fragments[count - 1].lasty,
											lastx = Process.Processes[i].fragments[count - 1].lastx - 1,
											lasty = Process.Processes[i].fragments[count - 1].lasty
										};
										Process.Processes[i].fragments.Add(newFrag);

									}
								}
								for (int j = 1; j < fragments.Count; j++)
								{
									if (fragments[0].x == fragments[j].x && fragments[0].y == fragments[j].y)
									{
										Process.Processes[i].tempBool = true;//end
										if(Process.Processes[i].tempInt > Process.Processes[i].tempList[4])
										{
											Process.Processes[i].tempList[4] = Process.Processes[i].tempInt;
											File.WriteAllText(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat", Process.Processes[i].tempInt.ToString());
										}
											
									}
								}

								if (fragments[0].x < 30 && fragments[0].x > -1 && fragments[0].y < 31 && fragments[0].y > 1)
								{

								}
								else
								{
									Process.Processes[i].tempBool = true;//end
									if (Process.Processes[i].tempInt > Process.Processes[i].tempList[4])
									{
										Process.Processes[i].tempList[4] = Process.Processes[i].tempInt;
										File.WriteAllText(@"0:\Users\" + Kernel.loggedUser + @"\Saved\Snake\Save.dat", Process.Processes[i].tempInt.ToString());
									}
								}
							}
							else
								Process.Processes[i].tempInt2++;
							Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightlightMain, X + fragments[0].x * 30, Y + fragments[0].y * 30, 27, 27);

							for (int j = 0; j < fragments2.Count; j++)
							{
								Explorer.CanvasMain.DrawFilledRectangle(Color.IndianRed, X + fragments2[j].x * 30, Y + fragments2[j].y * 30, 27, 27);
							}


							for (int j = 1; j < fragments.Count; j++)
							{
								if (update)
								{
									fragments[j].lasty = fragments[j].y;
									fragments[j].lastx = fragments[j].x;

									fragments[j].x = fragments[j - 1].lastx;
									fragments[j].y = fragments[j - 1].lasty;
								}
								Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + fragments[j].x * 30, Y + fragments[j].y * 30, 27, 27);

							}

							StringsAcitons.DrawCenteredTTFString(Process.Processes[i].tempInt.ToString(), SizeX, X, Y + 70, 15, Color.White, "CB", 25);

						}
						else if (!Process.Processes[i].tempBool && Kernel._fps > Process.Processes[i].tempList[1])
						{
							StringsAcitons.DrawCenteredString("Game paused.", SizeX, X, Y + (SizeY - 50) / 2 - 9, 15, Color.Red, Kernel.fontRuscii);
							StringsAcitons.DrawCenteredString("Please select this window to continue playing!", SizeX, X, Y + (SizeY - 50) / 2 + 9, 15, Color.White, Kernel.fontRuscii);
						}
						else if (Kernel._fps > Process.Processes[i].tempList[1])
						{
							StringsAcitons.DrawCenteredString("You lost!", SizeX, X, Y + (SizeY - 50) / 2 - 9, 15, Color.Red, Kernel.fontRuscii);
							StringsAcitons.DrawCenteredString("Points: " + Process.Processes[i].tempInt + " Best: " + Process.Processes[i].tempList[4], SizeX, X, Y + (SizeY - 50) / 2 + 9, 15, Color.White, Kernel.fontRuscii);
                            Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + (SizeX / 2) - 50, Y + SizeY / 2 + 30, 100, 30);
                            StringsAcitons.DrawCenteredString("Play Again", SizeX, X, Y + SizeY / 2 + 38, 15, Kernel.fontColor, Kernel.fontRuscii);
                            Explorer.CanvasMain.DrawFilledRectangle(Kernel.lightMain, X + (SizeX / 2) - 50, Y + SizeY / 2 + 70, 100, 30);
                            StringsAcitons.DrawCenteredString("Menu", SizeX, X, Y + SizeY / 2 + 78, 15, Kernel.fontColor, Kernel.fontRuscii);
							if(Cosmos.System.MouseManager.MouseState == Cosmos.System.MouseState.Left)
							{
								if(Cosmos.System.MouseManager.X >= X + (SizeX / 2) - 50 && Cosmos.System.MouseManager.X <= X + (SizeX / 2) + 50)
								{
									if(Cosmos.System.MouseManager.Y >= Y + SizeY / 2 + 30 && Cosmos.System.MouseManager.Y <= Y + SizeY / 2 + 60)
									{
										PlayAgain(i);
									}
								}
								if(Cosmos.System.MouseManager.X >= X + (SizeX / 2) - 50 && Cosmos.System.MouseManager.X <= X + (SizeX / 2) + 50)
								{
									if(Cosmos.System.MouseManager.Y >= Y + SizeY / 2 + 70 && Cosmos.System.MouseManager.Y <= Y + SizeY / 2 + 100)
									{
										Menu(i);
									}
								}
							}
                        }
						else
						{
							StringsAcitons.DrawCenteredString("LOADING...", SizeX, X, Y + (SizeY - 50) / 2 - 9, 15, Color.Red, Kernel.fontRuscii);
							StringsAcitons.DrawCenteredString("Loading snake", SizeX, X, Y + (SizeY - 50) / 2 + 9, 15, Color.White, Kernel.fontRuscii);
						}
						#endregion
					}
					
					break;
			}
			

		}
		public static void PlayAgain(int i)
		{
            Process.Processes[i].fragments = new List<fragment>();
            Process.Processes[i].fragments2 = new List<fragment>();
            fragment frag = new fragment
            {
                x = 16,
                y = 15,
                lastx = 15,
                lasty = 14
            };
            fragment frag2 = new fragment
            {
                x = 15,
                y = 15,
                lastx = 14,
                lasty = 14
            };
            Process.Processes[i].fragments.Add(frag);
            Process.Processes[i].fragments.Add(frag2);
            Random random = new Random();
            for (int o = 0; o < Process.Processes[i].tempList[3]; o++)
            {
                int x1 = random.Next(0, 30);
                int y1 = random.Next(2, 31);
                fragment fragApple = new fragment
                {
                    x = x1,
                    y = y1
                };
                Process.Processes[i].fragments2.Add(fragApple);
            }
            for (int o = 0; o < Process.Processes[i].tempList[3]; o++)
            {
                for (int j = 0; j < Process.Processes[i].tempList[3]; j++)
                {
                    if (o != j && Process.Processes[i].fragments2[o].x == Process.Processes[i].fragments2[j].x)
                    {
                        Process.Processes[i].fragments2[i].x = random.Next(0, 30);
                    }

                    if (o != j && Process.Processes[i].fragments2[o].y == Process.Processes[i].fragments2[j].y)
                    {
                        Process.Processes[i].fragments2[i].y = random.Next(2, 31);
                    }
                }
            }
            Process.Processes[i].tempInt = 1;
            Process.Processes[i].tempInt3 = 0;
            Process.Processes[i].tempBool = false;
			Start(Process.Processes[i].ID);
        }
		public static void Menu(int i)
		{
            Process.Processes[i].fragments = new List<fragment>();
            Process.Processes[i].fragments2 = new List<fragment>();
            fragment frag = new fragment
            {
                x = 16,
                y = 15,
                lastx = 15,
                lasty = 14
            };
            fragment frag2 = new fragment
            {
                x = 15,
                y = 15,
                lastx = 14,
                lasty = 14
            };
            Process.Processes[i].fragments.Add(frag);
            Process.Processes[i].fragments.Add(frag2);
            Process.Processes[i].tempInt = 1;
            Process.Processes[i].tempInt3 = 0;
            Process.Processes[i].tempBool = false;
            Process.Processes[i].tempList[2] = 0;
			Start(Process.Processes[i].ID);
        }
	}


	public class fragment
	{
		public int x, y;
		public int lastx, lasty;
	}
}
