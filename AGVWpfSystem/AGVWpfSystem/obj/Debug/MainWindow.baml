   M S B A M L   `   `   `  ����G  CAGVWpfSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:AGVWpfSystemAGVWpfSystem  5   	       AGVWpfSystem.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:AGVWpfSystem     5   	   <dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars  5   	   c _DevExpress.Xpf.Ribbon.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a>dxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon  5   	   <dxc2http://schemas.devexpress.com/winfx/2008/xaml/core  5   	   ?dxe5http://schemas.devexpress.com/winfx/2008/xaml/editors  5	   	     � Title$  
MainWindow��5   	   $��613.667��6   $��923.4��6-    � Icon 5
   	   *  $DevExpress.Xpf.Core.DXImageExtension     Image1  +DevExpress.Xpf.Core.Native.DXImageConverter$ Home_16x16.png .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   )  #DevExpress.Xpf.Ribbon.RibbonControl  -   5          Name$ RibbonControl��5      $��0��63      ApplicationButtonText$
 File��5          PageCategoryAlignment$ Right=�5          RibbonStyle$ 
Office2010=�5       !   ApplicationButtonSmallIcon 5         $ Home_16x16.png !	   ApplicationButtonLargeIcon	 5         $ Home_32x32.png 
   ApplicationMenu
 5      +  %DevExpress.Xpf.Ribbon.ApplicationMenu  5         RightPaneWidth$	 280Q�6&      ShowRightPane . #  DevExpress.Xpf.Bars.PopupMenu   	ItemLinks 5      +  %DevExpress.Xpf.Bars.BarButtonItemLink  5      %  DevExpress.Xpf.Bars.BarItemLink   BarItemName$
 bNew��60     5      $ bOpen��60   .  (DevExpress.Xpf.Bars.BarItemLinkSeparator  5       0	  *DevExpress.Xpf.Bars.BarSplitButtonItemLink	  5!      $ sbSave��65     5"      $ bPrint��60     5$        5%      $ bAbout��60      	RightPane 5'      �� 5(      
�������$��1,0,0,0q�64   
�������._�5)      �  5*   "   �� 5+   &   $
��Auto �64   �� 5,   &   $��* �64   .+�5.   "   �� $��0,0,0,1q�6)   
�������$��7,5,5,0q�6[   ._�5/   &   �� $
��Bold"�6,   .��6>   Recent Documents:6Q   50   #   �� 51   "   $��1��6*   $��0q�67   $��2,0,0,0q�6K   ��52   &   �� 53   *   .R�54   .   ��  �� Orientation$ 
Horizontal=�69   $��0,2,0,0q�6R   .+�55   2   �� $��0,0,0,1q�69   
���   �._�56   6   �� ��6@   �� *Number+57   3   �� 58   2   $��7,0,0,0q�6V   ��6<   �� *
FileName+6V   59   /   5:   +   @�5<   &   5C   #   5D      5E         
BottomPane 5G      �� 5H      $ 
Horizontal=�6%   .+�5I      �� -   5        $��Options��5I   A   $	��100��6S   $��0,0,10,0q�6_   �� 5K      -   5        $
��Exit��5K   >   $	��100��6M   5M      5O         ToolbarItemLinks 5W        5X      $ bOpen��6(     5Y      $ bSave��6(      
Categories. 5^      5
  /DevExpress.Xpf.Ribbon.RibbonDefaultPageCategory
  2  ,DevExpress.Xpf.Ribbon.RibbonPageCategoryBase   Pages. 5_      &   DevExpress.Xpf.Ribbon.RibbonPage     Caption)  #DevExpress.Xpf.Core.ObjectConverter$
 Home 6!      Groups. 5`      +  %DevExpress.Xpf.Ribbon.RibbonPageGroup  -   5         �Name$ pgFile��5`   *      Caption$
 File��68      ShowCaptionButton .    Items. 5d      '  !DevExpress.Xpf.Bars.BarButtonItem  -   5        $
 bNew��5d   ,   !  DevExpress.Xpf.Bars.BarItem   Content$	 New 68      Description$ Creates a new document. 5g          Hint$ Creates a blank document. 5h          RibbonStyle$ Large=�5i          Glyph 5e         $ New_16x16.png     
LargeGlyph  5f         $ New_32x32.png 5i         5k      -   5        $ bOpen��5k   ,   $
 Open 69   $ Opens a file. 5n       $ Opens a file. 5o       $ SmallWithText=�5p        5l         $ Open_16x16.png   5m         $ Open_32x32.png 5p         5r      -   5        $ bClose��5r   ,   $ Close 6:   $! Closes the current document 5u       $ SmallWithText=�5v        5s         $ Close_16x16.png   5t         $ Close_32x32.png 5v         5x      -   5        $ bPrint��5x   ,   $ Print 6:   $ Prints the document. 5{       $ Prints the document. 5|       $ SmallWithText=�5}        5y         $ Print_16x16.png   5z         $ Print_32x32.png 5}         5      ,  &DevExpress.Xpf.Bars.BarSplitButtonItem  5�      -	   5        $ sbSave��5�   1   $
 Save 6?   $ Large=�5�   %    5�   %     $ Save_16x16.png   5�   %     $ Save_32x32.png !   PopupControl! 5�        5�   "   "   Items." 5�   &     -
   5        $ bSave��5�   8   $
 Save 6E   $ Saves the document. 5�   )   $ Saves the document. 5�   )    5�   )     $ Save_16x16.png   5�   )     $ Save_32x32.png 5�   )     5�   &   -   5        $ bSaveAs��5�   8   $ 
Save As... 6G   $ Save Document As... 5�   )   $ Save Document As... 5�   )    5�   )     $ SaveDialog_16x16.png   5�   )     $ SaveDialog_32x32.png 5�   )   5�   #   5�      5�        5�      -   5        $
 Edit��5�   *    . . 5�        -   5        $ bPaste��5�   ,   $ Paste 6:   $ Large=�5�   Q    5�         $ Paste_16x16.png   5�         $ Paste_32x32.png 6Q     5�      -   5        $
 bCut��5�   ,   $	 Cut 68   $ SmallWithText=�5�   K    6      $ Cut_16x16.png 6K     5�      -   5        $ bCopy��5�   ,   $
 Copy 69   $ SmallWithText=�5�   L    6      $ Copy_16x16.png 6L     5�      -   5        $ bClear��5�   ,   $ Clear 6:   $ SmallWithText=�5�   N    6      $ Delete_16x16.png 6N   5�        5�      $ Format��6*    .  . 5�      *  $DevExpress.Xpf.Ribbon.BarButtonGroup  -   5        $ bgFontShape��5�   -   $ SmallWithoutText=�6@   #   Items.# 5�      &   DevExpress.Xpf.Bars.BarCheckItem  -   5        $ bBold��5�   /   $
 Bold 6<    5�        $ Bold_16x16.png   5�      -   5        $ bItalic��5�   /   $ Italic 6>    5�        $ Italic_16x16.png   5�      -   5        $ 
bUnderline��5�   /   $ 	Underline 6A    5�        $ Underline_16x16.png 5�      5�      5�      5�      .  (DevExpress.Xpf.Ribbon.RibbonPageCategory  5�      -   5        $  Name$$ categorySelection��5�   %   %   Caption$% 	Selection��5�   )   &   Color$& Yellow��5�   )   '   	IsVisible' .  . 5�        $ Gallery Page 6!   . 5�        $
 Font��6*    .  . 5�      0  *DevExpress.Xpf.Ribbon.RibbonGalleryBarItem  -   5        $ gFont��5�   3   $
 Font 6v    6@     $ ChangeFontStyle_16x16.png (   Links( 5�      )  #DevExpress.Xpf.Bars.BarEditItemLink  5�   "   $ 	eFontSize��66   )   RibbonStyle$) SmallWithText=�6N   *   Gallery* 5�      !  DevExpress.Xpf.Bars.Gallery  5�   "   -   5        +   MinColCount$+ 1��5�   .   ,   ColCount$, 5��6>   -   ItemCheckMode$- Single=�5�   "   .   IsGroupCaptionVisible] YDevExpress.Data.v17.2, Version=17.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a5  /DevExpress.Utils.Design.DefaultBooleanConverter$. False 5�   "   /   IsItemCaptionVisible/ .  0   IsItemDescriptionVisible0 .  #1   ItemCaptionVerticalAlignment$1 Center=�5�   "   2   Groups2 5�   &   *  $DevExpress.Xpf.Bars.GalleryItemGroup  5�   *   -   5        $ FontFamilyGalleryGroup��5�   ?   5�   #   3   DropDownGallery3 5�        5�   "   -   5        $+ 1��5�   .   $, 1��6>   4   RowCount$4 5��5�   "   $- Single=�5�   "   $. False 5�   "   5   AllowFilter5 .  6   IsItemGlyphVisible6 . 0 .  %7   ItemContentHorizontalAlignment$7 Stretch=�5�   "   #8   ItemContentVerticalAlignment$8 Center=�5�   "   9   SizeMode$9 Vertical=�5�   "   2 5�   &     5�   *   -   5        $$ FontFamilyDropDownGalleryGroup��5�   ?   5�   #   5�      %  DevExpress.Xpf.Bars.BarEditItem  5�      -   5        $ 	eFontSize��5�   *   $ SmallWithoutText=�6;   :   	EditWidth$: 90X�6Z   $ 
Font Size: 5�      ;   EditSettings; 5�      :  4DevExpress.Xpf.Editors.Settings.ComboBoxEditSettings  5�   "   ;  5DevExpress.Xpf.Editors.Settings.PopupBaseEditSettings<   PopupMaxHeight$	< 250Q�6;   8  2DevExpress.Xpf.Editors.Settings.ButtonEditSettings=   IsTextEditable$= FalseX�6P   5�      5�      5�      5�      5�      2  ,DevExpress.Xpf.Ribbon.RibbonStatusBarControl  5�   
   -   5        $��	StatusBar��5�   2   $��2��6%   >   IsSizeGripVisible> . $��2��6\   ?   
RightItems? 5�      '  !DevExpress.Xpf.Bars.BarStaticItem  5�      -   5        $ 	bAgvState��5�   $   @   ItemMinWidth$	@ 150Q�65   A   AutoSizeMode$
A Fill=�6H   $ AGV连接状态：在线 6\     5�      -   5        $ bRobotState��5�   $   $ 机器人状态：在线 67   B   	LeftItemsB 5�        5�      -   5        $ bPosInfo��5�   $   $	@ 150Q�64   5�      � 5�   
   $��1��6   C � ColumnDefinitionsC 5�      �� 5�      $
��620* �6#   �� 5�      $
��297* �6#    5�      �� 5      $
��Auto �6    �� 5     $
��259* �6    �� 5     $
��163* �6    �� 5     $
��Auto �6    .+�5     �� -    5        $��drawingSurface��5     $��4��6+   $��
0,0,7,-0.4q�6<   
�������.+�5     �� 
��� ���
����  �$��14��68   $��17��6D   D �� Left$	D 292��6O   
E �� Top$	E 110��6a       AGVWpfSystem.CPoint   5     
��� ���$��4��6-   $	D 258��68   
����  �$E 83��6X   $��8��6h   !   AGVWpfSystem.CArrowLine!  5     $��14��6#   $D 51��6/   
����  �$E 93��6N   $	��177��6^   F J� RenderTransformOrigin$F 0.5,0.5&�6j   5	     "   AGVWpfSystem.ShowInfoView"  5
     $
��Left=�6!   $	��248��6<   $��2��6I   $	��Top=�6Z   $	��282��6r   $��5,0,0,0q�6~   $��1��6�    #   AGVWpfSystem.RobotInfoView#  5     $��1��6"   $��5,6.6,52.8,-0.4q�62   $��2��6K   $��2��6X   5     5     5     