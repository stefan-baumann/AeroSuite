![AeroSuite Logo](http://www.vb-paradise.de/index.php/Attachment/32001-AeroSuite-Logo-png/)
<p>AeroSuite is a WinForms Control Library that provides extended functionality for pre-existing controls and also new controls. Unlike most (if not all) similar libraries, this library is aimed at supporting every platform: Windows XP to 10 and Linux with Mono are supported.</p>
<p>Every control is drawn natively by the system. If this is not supported by the operating system, it will either automatically fall back to a custom-drawn version or simply not provide the feature (only if it is not essential for the user experience). That allows the use on every platform. In the far future, I'm also planning on creating a workaroung for every added feature to improve the user experience even more.</p>
<p>There are not many controls in the library right now as it is still in a early stage of development. About 20 more controls are already implemented and just waiting to be revised and released.</p>
<p>Although this library is released under the MIT license that allows you to do almost anything with it I'd like to be mentioned in your about-dialog/credits/whatever as the creator of this library.</p>

# Planned
- Add more controls (the obvious task)
- More cross-platform compatibility/support
- Add screenshots from different platforms to the control documentation
- Provide class diagrams for the important things

# Controls
The following controls are currently available:
- [AeroLinkLabel](#aerolinklabel)
- [AeroListView](#aerolistview)
- [AeroProgressBar](#aeroprogressbar)
- [AeroTreeView](#aerotreeview)
- [BottomPanel](#bottompanel)
- [CueComboBox](#cuecombobox)
- [CueTextBox](#cuetextbox)
- [HeaderlessTabControl](#headerlesstabcontrol)
- [NavigationButton](#navigationbutton)
- [Seperator](#seperator)

## AeroLinkLabel
The AeroLinkLabel is a link label with improved styling: The colors are either extracted from the system's visual styles (`TextStyle > HyperLinkText`) or taken from the SystemColors-class. It also fixes Microsoft's fault with the hand cursor by using the system's hand cursor.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support
- [x] Linux (Mono) support

## AeroListView
The AeroListView currently does exactly what the "normal" ListView does but it does that in a far more stylish way as it is styled according to the current (Windows) theme. On non-windows systems there are no changes made.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support
- [x] Linux (Mono) support

## AeroProgressBar
The AeroProgressBar is a progress bar with the ability to set a state: normal, paused or error. It also uses a feature of some operating systems (Windows Vista and higher) to make it go backwards smoothly. On non-windows systems there are no changes made. I'm not planning on providing this feature in the near future as there are many limitations to circumvent.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [ ] Windows XP & Classic support (not natively supported by the os)
- [ ] Linux (Mono)  (not natively supported by the os)

## AeroTreeView
Same as the AeroListView applied to a TreeView.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support
- [x] Linux (Mono) support

## BottomPanel
A Panel that you can put - as the name suggests - on the bottom of a form for seperating some controls from the others on the form. When using Windows Vista or higher, it is drawn by the system via visual styles (`TaskDialog > SecondaryPanel`). Alternatively it uses the system colors to manually draw the panel.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support
- [x] Linux (Mono) support

## CueComboBox
A ComboBox with cue banner support. That means that when there is no item selected, a text will be shown as a placeholder ("cue banner"). You can change the text with the `Cue` property.
As this feature is only supported on systems that support windows aero, this feature will not work on other systems. It is still possible to use it like a normal ComboBox nevertheless. I'm working on a fix for that problem and will release an update as soon as I have found a way to manually show such a text.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [ ] Windows XP & Classic support (not natively supported by the os)
- [ ] Linux (Mono) support (not natively supported by the os)

## CueTextBox
A TextBox with cue banner support. Similar to the CueComboBox, it will show a greyed-out text when there is no text in it. The text that shown is accessible through the `Cue` property. With the `RetainCue` property you can specify if the banner is shown when the textbox is focused.
As with the CueComboBox, I'm working on a manual way to show such a text manually on systems that do not support this feature natively.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support (partial support: the `RetainCue` property is not supported)
- [ ] Linux (Mono) support (not natively supported by the os)

## HeaderlessTabControl
A tab control that does not have tab headers at run time. For usability purposes, the headers are shown at design time.
This control works on every Windows version without problems. On Linux (at least Linux Mint) there is a problem with the headers not being fully hidden.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support
- [x] Linux (Mono) support (due to a bug, the tab headers are still partially shown)

## NavigationButton
A round button with an arrow in it that either points backwards or forwards as you know it from the Windows Explorer. As it is drawn by the system via visual styles (`Navigation > BackButton / ForwardButton`), it adapts to the windows theme. Alternatively it uses a simple custom Renderer to draw it.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support (via custom drawing)
- [x] Linux (Mono) support (via custom drawing)

## Seperator
A simple horizontal seperator line. As it is drawn by the system via visual styles (`TaskDialog > FootnoteSeperator`), it adapts to the windows theme. Alternatively it uses a simple custom Renderer to draw it.
- [x] Windows Aero support (Vista, 7, 8, 10)
- [x] Windows XP & Classic support (via custom drawing)
- [x] Linux (Mono) support (via custom drawing)
