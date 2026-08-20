using Dalamud.Bindings.ImGui;
using Dalamud.Game.ClientState.Objects.Types;
using ECommons.DalamudServices;
using ECommons.GameHelpers;
using NecroLens.Enums;
using NecroLens.MobData;
using NecroLens.util;
using Pictomancy;
using System.Drawing;
using System.Numerics;
using static NecroLens.MobData.MobDatabase;

namespace NecroLens.PictoRender
{
    internal static partial class PictoManager
    {
        private static uint CurrentTerritory = 0;

        public static void DrawAggroInfo(string id, IGameObject mobData, MobInfo mobInfo)
        {
            if (Player.Available)
                CurrentTerritory = Player.Territory.RowId;

            var aggroInfo = mobInfo.GetAggroInfo(CurrentTerritory);
            var aggroType = aggroInfo.AggroType;
            var enemyType = mobInfo.MobType;

            float aggroRange = enemyType is ESPType.Mimic && DeepDungeonUtil.InPotD ? 14 : 10;

            var hitbox = mobData.HitboxRadius;
            var position = mobData.Position;

            float size = hitbox + aggroRange;

            if (aggroType is AggroType.Sight)
            {
                uint color = C.NormalAggroColor;
                Vector4 colorV = ImGui.ColorConvertU32ToFloat4(color);
                const float zoneRadian = 1.571f; // ~90° aggro cone

                // er_gl_fan100_o0v fan

                AddDrawCommand(pictoDraw =>
                {
                    var coneColor = color.SetAlpha(0.2f);
                    var rotation = -mobData.Rotation;
                    float halfAngle = zoneRadian / 2f;
                    float minAngle = rotation - halfAngle;
                    float maxAngle = rotation + halfAngle;


                    // pictoDraw.AddConeFilled(position, size, rotation, zoneRadian, coneColor, coneColor);

                    // Works, but doesn't redraw while moving so... minor issue *-sighs-*
                    PctService.VfxRenderer.AddFan(id, position, 0f, size, minAngle, maxAngle, colorV);
                    // PctService.VfxRenderer.AddCustom(id, vfxFan, position, new(size), rotation, colorV);


                });
            }
            else if (aggroType is AggroType.Sound)
            {
                uint color = C.SoundAggroColor;
                AddDrawCommand(pictoDraw =>
                {
                    pictoDraw.AddCircleFilled(position, size, color);
                });
            }
            else if (aggroType is AggroType.Proximity)
            {
                uint color = C.NormalAggroColor;
                AddDrawCommand(pictoDraw =>
                {
                    pictoDraw.AddCircle(position, size, color);
                });
            }

            AddDrawCommand(pictoDraw =>
            {
                position = new(position.X, position.Y - 0.5f, position.Z);
                pictoDraw.AddText(position, Color.White.ToUint(), $"{mobData.Name}\n ID: {mobData.BaseId}", 1);
            });
        }

        public static void TestCircle()
        {
            if (Player.Available)
            {
                ImGui.Text("Player available");
                var size = 10;
                var position = Player.Position;

                AddDrawCommand(pictoDraw =>
                {
                    pictoDraw.AddCircleFilled(position, 20, C.SoundAggroColor, C.SoundAggroColor);
                });

            }
        }
    }
}
