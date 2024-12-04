using iTextSharp.text;
using iTextSharp.text.pdf;
using LogApp;
using Secure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using vSuperMTClient.Entities;

namespace vSuperMTClient
{
    public partial class ReportsCommonMethods
    {
        public static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        public static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 5f;
            cell.PaddingTop = 5f;
            return cell;
        }
        public static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        public static PdfPCell RowCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 3f;
            Cell.PaddingBottom = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }
        public static PdfPCell RowCellBGColoredTrail(string s, bool SetBackColor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            if (SetBackColor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
               // Cell.PaddingTop = 0f;
            }
            return Cell;
        }


        public static PdfPCell RowLeftCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell RowLeftCellSub2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 2f;
            Cell.PaddingBottom = 2f;
            //Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }
        public static PdfPCell RowLeftCellSub(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 2f;
            Cell.PaddingBottom = 2f;
            Cell.PaddingLeft = 20f;
            //Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }
        public static PdfPCell RowLeftCellBottom(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell RowLeftCellBottom2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 2f;
            Cell.PaddingBottom = 2f;
            return Cell;
        }

        public static PdfPCell RowLeftCell2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell EmptyRowLeftCellTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell RowLeftPaddingCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingLeft = 25f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }

        public static PdfPCell RowLeftPaddingCell2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingLeft = 15f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowLeftCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowLeftCellBold2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 2f;
            Cell.PaddingBottom = 2f;
            Cell.PaddingLeft = 20f;
            return Cell;
        }
        public static PdfPCell RowLeftCellBoldBottom(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell RowLeftCellBoldBottom2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 2f;
            Cell.PaddingBottom = 2f;
            Cell.PaddingLeft = 20f;
            return Cell;
        }

        public static PdfPCell RowLeftCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 3f;
            Cell.PaddingBottom = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }

        public static PdfPCell RowLeftCellBGColoredTrail(string s,bool SetBackColor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            if (SetBackColor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
               // Cell.PaddingTop = 0f;
            }
            return Cell;
        }
        public static PdfPCell RowRightCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowRightCell2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            Cell.PaddingRight = 20f;
            return Cell;
        }
        public static PdfPCell RowRightCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPCell RowRightCellBold2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            Cell.PaddingRight = 20f;
            return Cell;
        }

        public static PdfPCell RowRightCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 3f;
            Cell.PaddingBottom = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }

        public static PdfPCell RowRightCellBGColoredTrail(string s,bool SetBackColor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            if (SetBackColor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
             //   Cell.PaddingTop = 0f;
            }
            return Cell;
        }

        public static PdfPCell BottomRowCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            

            return Cell;
        }
        public static PdfPCell BottomRowCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;


            return Cell;
        }

        public static PdfPCell BottomRowCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 3f;
            Cell.PaddingBottom = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;

            return Cell;
        }
        public static PdfPCell BottomRowLeftCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            return Cell;
        }

        public static PdfPCell BottomRowCellCenter(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.VerticalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            return Cell;
        }
        public static PdfPCell BottomRowLeftCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            return Cell;
        }

        public static PdfPCell BottomRowLeftCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);// Color.LIGHT_GRAY;
            return Cell;
        }
        public static PdfPCell BottomRowRightCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            return Cell;
        }
        public static PdfPCell BottomRowRightCellBold(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            return Cell;
        }

        public static PdfPCell BottomRowRightCellBold2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            Cell.PaddingRight = 20f;
            return Cell;
        }

        public static PdfPCell BottomRowRightCellBoldBGColoredTrail(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            Cell.BackgroundColor = new Color(223, 223, 223);// Color.LIGHT_GRAY;
            return Cell;
        }

        public static PdfPCell BottomRowLeftCellBGColoredTrail(string s,bool setbackcolor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            if (setbackcolor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);// Color.LIGHT_GRAY;
               // Cell.PaddingTop = 0f;
            }
            return Cell;
        }

        public static PdfPCell BottomRowLeftCellBGColored(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            Cell.BackgroundColor = new Color(223, 223, 223);// Color.LIGHT_GRAY;
            return Cell;
        }

        public static PdfPCell BottomRowLeftCellBGColored2(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            Cell.PaddingLeft = 20f;
            Cell.BackgroundColor = new Color(223, 223, 223);// Color.LIGHT_GRAY;
            return Cell;
        }

        public static PdfPCell BottomRowCellBGColoredTrail(string s, bool SetBackColor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 3f;
            Cell.PaddingBottom = 3f;
            if (SetBackColor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
              //  Cell.PaddingTop = 0f;
            }

            return Cell;
        }

        public static PdfPCell BottomRowCellBGColored(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 5f;
            Cell.PaddingBottom = 5f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;

            return Cell;
        }

        public static PdfPCell BottomRowRightCellBGColoredTrail(string s,bool SetBackColor)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.DARK_GRAY)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 3f;
            Cell.PaddingTop = 3f;
            if (SetBackColor)
            {
                Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
               // Cell.PaddingTop = 0f;
            }
            return Cell;
        }

        public static PdfPCell BottomRowRightCellBGColored(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingBottom = 5f;
            Cell.PaddingTop = 5f;
            Cell.BackgroundColor = new Color(223, 223, 223);//Color.LIGHT_GRAY;
            return Cell;
        }
        public static PdfPCell FirstHeaderRowCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell FirstHeaderRowCellBold(string s , float fontSize , int align, float paddingleft,float paddingRight)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Calibri", fontSize, Font.BOLD, Color.BLACK)));
            Cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            Cell.HorizontalAlignment = Element.ALIGN_BOTTOM;
            
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingLeft = paddingleft;
            Cell.PaddingRight = paddingRight;
            
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell FirstHeaderRowCellBold2(string s, float fontSize, float paddingleft, float paddingRight)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Calibri", fontSize, Font.BOLD, Color.BLACK)));
            Cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;

            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingLeft = paddingleft;
            Cell.PaddingRight = paddingRight;

            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell FirstHeaderRowLeftCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;

            return Cell;
        }
        public static PdfPCell HeaderRowCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell HeaderRowLeftCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell HeaderRowLeftCellBOLD(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }

        public static PdfPCell HeaderRowRightCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell HeaderRowLeftCellNotRotated(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            return Cell;
        }
        public static PdfPCell HeaderRowCellRotated(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
            Cell.HorizontalAlignment = Element.ALIGN_LEFT;
            Cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            Cell.Border = Rectangle.BOTTOM_BORDER;
            Cell.PaddingTop = 0f;
            Cell.PaddingBottom = 5f;
            Cell.Rotation = 90;
            return Cell;
        }
        public static PdfPCell EmptyDataCell(string s)
        {
            PdfPCell Cell = new PdfPCell(new Phrase(s, FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.RED)));
            Cell.HorizontalAlignment = Element.ALIGN_CENTER;
            Cell.Border = Rectangle.NO_BORDER;
            Cell.PaddingTop = 50f;
            Cell.PaddingBottom = 0f;
            return Cell;
        }
        public static PdfPTable EmptyDataTable()
        {
            PdfPTable Datatable = new PdfPTable(1);
            Datatable.TotalWidth = 782;
            Datatable.LockedWidth = true;
            Datatable.HorizontalAlignment = Element.ALIGN_CENTER;
            Datatable.SetWidths(new float[] { 1f });
            Datatable.SpacingBefore = 15f;
            Datatable.AddCell(ReportsCommonMethods.EmptyDataCell("No data available for your selected criteria."));
            return Datatable;
        }
        
        public static string GetHourFormat(string s)
        {
            int Hour = int.Parse(s);
            string Hour1 = TimeSpan.FromSeconds(Convert.ToDouble(Hour * 60 * 60)).ToString(@"hh\:mm");
            string Hour2 = TimeSpan.FromSeconds(Convert.ToDouble((Hour + 1) * 60 * 60)).ToString(@"hh\:mm");
            return Hour1 + " - " + Hour2;
        }
        //public static List<AvailableLicenses> GetAvailableLicenses()
        //{
        //    List<AvailableLicenses> AvailableLicensesList = new List<AvailableLicenses>();
        //    string ClientDB = HttpContext.Current.Session["vSupervisorDB"].ToString();
        //    string vSupervisorDB = System.Configuration.ConfigurationManager.AppSettings["vSupervisorDB"];
        //    string ConnectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString.Replace(vSupervisorDB, ClientDB);
        //    LicInformation objLicInformation = new LicInformation(ConnectionString, ClientDB);
        //    try
        //    {
        //        foreach (LicInformation.ServerLicense serverLicense in LicInformation.EnumToList<LicInformation.ServerLicense>())
        //        {
        //            string ModuleName = LicInformation.GetEnumDescription(serverLicense).Name;
        //            string[] KeyDetail = objLicInformation.getKeyFromXML(ModuleName);
        //            if (KeyDetail != null)
        //            {
        //                AvailableLicenses AvailableLicensesObj = new AvailableLicenses();
        //                AvailableLicensesObj.License = KeyDetail[6];
        //                AvailableLicensesObj.Description = KeyDetail[13];
        //                AvailableLicensesObj.Status = KeyDetail[14];
        //                if (KeyDetail[14].ToLower() != LicInformation.ServerStatus.Full.ToString().ToLower())
        //                {
        //                    try
        //                    {
        //                        AvailableLicensesObj.License = "vBoard Server";
        //                        AvailableLicensesObj.Description = "Trial License";
        //                        DateTime dtSaved = Convert.ToDateTime(KeyDetail[11]);
        //                        DateTime dtExpire = Convert.ToDateTime(KeyDetail[12]);

        //                        if (dtSaved >= dtExpire || dtSaved > DateTime.Now)
        //                            AvailableLicensesObj.Status = "Expired";//LicInformation.ServerStatus.Expire.ToString();
        //                        else
        //                        {
        //                            TimeSpan ts = dtExpire - dtSaved;
        //                            double days = Math.Round(ts.TotalDays);
        //                            AvailableLicensesObj.Status = days + " days left";
        //                            if (days <= 1)
        //                            {
        //                                AvailableLicensesObj.Status = "Less than 24 hours left";
        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Log4Net.WriteException(ex);
        //                    }
        //                }
        //                else
        //                {
        //                    AvailableLicensesObj.Description = "Full License";
        //                }
        //                AvailableLicensesList.Add(AvailableLicensesObj);
        //            }

        //        }

        //        return AvailableLicensesList;

        //    }
        //    catch (Exception ex)
        //    {
        //        Log4Net.WriteException(ex);
        //        return null;
        //    }
        //}

        public static PdfPCell GraphImageCell(Image image, float scale, int align)
        {

            //image.SetDpi(10, 10);
            //image.ScaleToFit(280f,280f);
            //image.SetDpi(840, 840);
            //image.ScaleToFit(280f, 280f);
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_MIDDLE;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 10f;
            return cell;
        }
        public static string GetTimeFromSeconds(Double Sec)
        {
            try
            {
               
                return Math.Floor(TimeSpan.FromSeconds(Sec).TotalHours).ToString().PadLeft(2, '0') + TimeSpan.FromSeconds(Sec).ToString(@"\:mm\:ss");
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "";
            }
        }
        public static string GetMinutesFromSeconds(Double Sec)
        {
            try
            {
                int min = (int)(Sec / 60);
                int sec = (int)(Sec % 60);
                string minString = "00";
                string secString = "00";

                if (min < 10)
                {
                    minString = "0" + min.ToString();
                }
                else
                {
                    minString= min.ToString();
                }
                if (sec < 10)
                {
                    secString= "0" + sec.ToString();
                }
                else
                {
                    secString = sec.ToString();
                }

                string duration = "00:" + minString + ":" + secString;
                return duration;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "";
            }
        }
        public static string GetMinutesFromSeconds2(Double Sec)
        {
            try
            {
                int min = (int)(Sec / 60);
                int sec = (int)(Sec % 60);
                string minString = "00";
                string secString = "00";

                if (min < 10)
                {
                    minString = "0" + min.ToString();
                }
                else
                {
                    minString = min.ToString();
                }
                if (sec < 10)
                {
                    secString = "0" + sec.ToString();
                }
                else
                {
                    secString = sec.ToString();
                }

                string duration =  minString + ":" + secString;
                return duration;
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "";
            }
        }
        public static string GetCallStatus(int LastState,int InitialState, double Duration, string Direction)
        {
            try
            {
                EnumLastState LState = (EnumLastState)LastState;
                EnumInitialStates IState = (EnumInitialStates)InitialState;


                if (LState == EnumLastState.CONNECTED && Duration > 0)
                    return "Answered";
                else if (IState == EnumInitialStates.TRANSFERIN)
                    return "Transfer In";
                else if (IState == EnumInitialStates.TRANSFERCALL)
                    return "Transfer";
                else if (LState == EnumLastState.Hangup_TRANSFER && Direction == "Outbound")
                    return "Transfer Out";
                else if (LState == EnumLastState.RINGINGIN)
                    return "Not Answered";
                else if ((LState == EnumLastState.TRANSFER && Duration == 0) || LState == EnumLastState.CALL_DIVERTED)
                    return "Call Lost";
                else if (LState == EnumLastState.TRANSFER)
                    return "Call Transfer";
                else if (LState == EnumLastState.CALL_DIVERTED)
                    return "Call Diverted";
                else return "Not Known";


            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "Not Known";
            }
        }
        public static double ToSize(Int64 value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (Int64)unit));
        }
        public static string GetNextTime(string TimeKey,int TimeInterval)
        {
            try
            {
                DateTime date = DateTime.Parse(TimeKey, System.Globalization.CultureInfo.CurrentUICulture);
                return date.AddMinutes(TimeInterval).ToShortTimeString();
                
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return "00:00:00";
            }
        }
        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }
        public static List<ReportDiskUsageCostEntity> GetDiskUsageCosting(float DiskUsageCostPerMin, string RecordingPath)
        {
            try
            {

                //string RecordingPath = "";
                //string AccountID = "1";
                //string FTPRepoisory = "FTPRepository";
                LogApp.Log4Net.WriteLog("GetDiskUsageCosting: started", LogType.GENERALLOG);
                //RecordingPath = RecordingPath + @"\" + AccountID + @"\" + FTPRepoisory;
                List<ReportDiskUsageCostEntity> ReportEntityList = new List<ReportDiskUsageCostEntity>();
                if (Directory.Exists(RecordingPath))
                {
                    DirectoryInfo di = new DirectoryInfo(RecordingPath);

                    foreach (DirectoryInfo temp in di.GetDirectories())
                    {
                        ReportDiskUsageCostEntity ReportDiskUsageCostEntityObj = new ReportDiskUsageCostEntity();
                        string Folder = temp.FullName;

                        try
                        {
                            ReportDiskUsageCostEntityObj.Time_Stamp= DateTime.ParseExact(temp.Name, "yyMMdd", CultureInfo.InvariantCulture);
                            long length = Directory.GetFiles(Folder, "*", SearchOption.AllDirectories).Sum(t => (new FileInfo(t).Length));

                            ReportDiskUsageCostEntityObj.DiskUsage = Math.Round( ToSize(length, SizeUnits.KB),2);
                            ReportDiskUsageCostEntityObj.Day = DateTime.ParseExact(temp.Name, "yyMMdd", CultureInfo.InvariantCulture).DayOfWeek.ToString();
                            ReportDiskUsageCostEntityObj.Cost = Math.Round(DiskUsageCostPerMin * ReportDiskUsageCostEntityObj.DiskUsage/180,2);
                            ReportEntityList.Add(ReportDiskUsageCostEntityObj);
                        }
                        catch (Exception ex)
                        {
                            Log4Net.WriteException(ex);
                        }
                    }
                    LogApp.Log4Net.WriteLog("GetDiskUsageCosting: completed", LogType.GENERALLOG);
                    return ReportEntityList;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Log4Net.WriteException(ex);
                return null;
            }
        }
      
    }
}