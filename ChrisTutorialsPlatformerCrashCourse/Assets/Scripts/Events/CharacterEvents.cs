using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents
{
    // TODO: Static event subscriptions can be prone to leak memory if forgotten to unsubscribe. Might want to figure out an alternative?

    /// <summary>
    /// First argument: the character;
    /// Second argument: the amount of damage.
    /// </summary>
    public static UnityAction<GameObject, int> CharacterDamaged;

    /// <summary>
    /// First argument: the character;
    /// Second argument: the amount healed.
    /// </summary>
    public static UnityAction<GameObject, int> CharacterHealed;
}