using Il2Cpp;
using System.Reflection;
using UnityEngine;

namespace QualityOfLife
{
    public static class ModInput
    {
        static bool CheckForUnityExplorer = true;
        static Func<bool>? UnityExplorerShowMenu = null;

        /// <summary>
        /// Finds UnityExplorer's UIManager.ShowMenu getter once and keeps it as a delegate.
        /// Searches the loaded assemblies directly, as AccessTools.TypeByName logs a warning when the type is missing.
        /// </summary>
        static Func<bool>? FindUnityExplorerShowMenu()
        {
            Type? TypeUIManager = AppDomain.CurrentDomain.GetAssemblies()
                .Select( Asm => Asm.GetType( "UnityExplorer.UI.UIManager", false ) )
                .FirstOrDefault( Found => Found != null );

            MethodInfo? Method = TypeUIManager?.GetProperty( "ShowMenu", BindingFlags.Public | BindingFlags.Static )?.GetMethod;
            if ( Method != null && Method.ReturnType == typeof( bool ) )
            {
                return Method.CreateDelegate<Func<bool>>();
            }
            return null;
        }

        public static bool ShouldProcessInput( KeyCode Key )
        {
            if ( CheckForUnityExplorer )
            {
                CheckForUnityExplorer = false;
                UnityExplorerShowMenu = FindUnityExplorerShowMenu();
            }

            if ( UnityExplorerShowMenu != null && UnityExplorerShowMenu() )
            {
                return false;
            }

            if ( uConsole.IsOn() )
            {
                return false;
            }

            Panel_Log PanelLog = InterfaceManager.GetPanel<Panel_Log>();
			if ( PanelLog != null && PanelLog.enabled && PanelLog.m_NotesTextField != null && PanelLog.m_NotesTextField.m_Input != null )
			{
				if ( PanelLog.m_NotesTextField.m_Input.isSelected )
				{
					return false;
				}
			}

            return Key != KeyCode.None;
        }

        public static bool GetKey( MonoBehaviour? Context, KeyCode Key )
        {
            if ( ShouldProcessInput( Key ) )
            {
                return Input.GetKey( Key );
            }
            return false;
        }

        public static bool GetKeyDown( MonoBehaviour? Context, KeyCode Key )
        {
            if ( ShouldProcessInput( Key ) )
            {
                return InputManager.GetKeyDown( Context, Key );
            }
            return false;
        }

        public static bool GetKeyUp( MonoBehaviour? Context, KeyCode Key )
        {
            if ( ShouldProcessInput( Key ) )
            {
                return Input.GetKeyUp( Key );
            }
            return false;
        }
    }
}
