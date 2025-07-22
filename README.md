Currently this mod contains Succubus & Incubus Xenotypes, but I may expand it in the future.

# Overview/Features
- 2 Xenotypes (Incubus & Succubus)
- Custom abilities (Polymorph for both, and Draining Kiss for Succubi). See their dedicated sections below for more details!
- Draining Kiss ability has the chance to turn the target into a Sanguophage!
- Custom genes (some courtesy of other mods - credits below!)
- No hemogenic genes! (you may or may not like that, but I didn't want these xenotypes to depend on hemogenic genes. However, if you want your Succubi to use hemogenic genes I recommend the great ReSplice: Charmweavers mod!)
- Custom sounds (so far ability sounds + succubus sounds)

# Requirements
- RimWorld v1.6.
- [Biotech](https://store.steampowered.com/app/1826140/RimWorld__Biotech/)
- [Royalty](https://store.steampowered.com/app/1149640/RimWorld__Royalty/)
- [Vanilla Expanded Framework](https://steamcommunity.com/sharedfiles/filedetails/?id=2023507013)
- [Vanilla Races Expanded - Archon](https://steamcommunity.com/sharedfiles/filedetails/?id=3067715093) (extremely aggressive, natural psylink, & maniac genes)

# Recommended
Mods I recommend if you wish them to appear as they do in the screenshot(s).
- [[NL] Facial Animation - WIP](https://steamcommunity.com/sharedfiles/filedetails/?id=1635901197)
- [[NL] Facial Animation - Experimentals](https://steamcommunity.com/sharedfiles/filedetails/?id=2581693737)
- [[BT] EyeGenes2 | Base - [NL] Facial Animation](https://steamcommunity.com/sharedfiles/filedetails/?id=2898151329)
- [UNAGI Facial Animation](https://steamcommunity.com/sharedfiles/filedetails/?id=3268178647)
- [UNAGI Facial Animation.biotech](https://steamcommunity.com/sharedfiles/filedetails/?id=3287187828)
- [UNAGI Vanilla Hairs](https://steamcommunity.com/sharedfiles/filedetails/?id=3386935298)
- [UNAGI_Hair1](https://steamcommunity.com/sharedfiles/filedetails/?id=3244456989)
- [UNAGI_Hair2](https://steamcommunity.com/sharedfiles/filedetails/?id=3248775397)
- [Vanilla Weapons Expanded (Whip)](https://steamcommunity.com/sharedfiles/filedetails/?id=1814383360)
- [Medieval Overhaul (other weapons)](https://steamcommunity.com/sharedfiles/filedetails/?id=3219596926)
- [Sized Apparel (for bodies/clothing)](https://gitgud.io/jikulopo/sized-apparel-zero)
- [Bread Mo's Valentine's Day apparel](https://steamcommunity.com/sharedfiles/filedetails/?id=3295274246)

# Compile Source
For developers, testers, or simply those who wish to build the assembly themselves.<br/>
This mod was built using Visual Studio Code and [https://github.com/Rimworld-Mods/Template](https://github.com/Rimworld-Mods/Template).<br/>
You need:
- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core)
- [.Net Framework 4.8 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- [Visual Studio Code - C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

Once the above is done, you just need to do this to build the assembly:
- Navigate to the .vscode directory and then type "dotnet build".

# Succubus & Incubus - Shared Genes
- Archite (Ageless, Archite metabolism, Non-senescent, Perfect immunity, Scarless, Natural psylink)
- Normal (Superfast wound healing, Psy-sensitive, Very unhappy, Maniac, Cold super-tolerant, Heat super-tolerant, Extremely aggressive, Very attractive, Strong stomach, Dark vision, Wings, Spiked tail, Claws, Red eyes)
- "Very unhappy" gene was added for metabolic cost but is also intended to express the difficulty of managing these xenotypes, rather than them being "unhappy". They rather like what they do in fact! I may replace this with a custom gene in the future.

# Succubus Only Genes
- Succubi are meant to be weaker than Incubi overall, but they do have the Draining Kiss ability, and are more socially adept than Incubi. (or rather, they prefer getting what they want without having to resort to violence). And of course, access to psy powers, but Incubi have those too.
- Succubus (human-form or demon-form), Mini-horns (varied), Awful mining, Great social, Thin or Standard body

# Incubus Only Genes
- Incubi are meant to be stronger than Succubi, but they are less socially adept than Succubi. (or rather, they prefer using brute force to get what they want)
- In my colony I use Incubi as frontline soldiers and equip Succubi with crossbows or whips, but Succubi can still hold their own, especially since they have access to psy powers as well.
- Incubus (human-form or demon-form), Horns (varied), Strong melee, Strong social, Standard or Hulk body

# Polymorph Ability (Both Xenotypes)
- Both xenotypes can polymorph into a human-looking pawn, meaning their horns, red eyes, red skin color, wings, and tail are disabled. Also, their horns don't change when polymorphing back and forth!
- When in their human form, they suffer no social penalties (-60 in demon form, if they have line of sight), but will lose the benefits of their wings and tail, for example. Their hunger rate will also slightly increase in this form.
- When in their demon form, they suffer no penalties except a -60 social penalty (if they have line of sight). I chose -60 for now as they are meant to be very beautiful but most people would still be struck by their appearance and see them as a threat. They would look like impids to most people, except with wings and tails. This will likely be expanded on in the future.
- The social penalty doesn't apply if both pawns are Succubi or Incubi.
- Psyfocus Cost: 1%
- Heat gain: 1
- Cooldown: 1 second

# Draining Kiss Ability (Succubi)
- This signature ability of the Succubi can drain their target of all their psyessence (your psyfocus will be refilled), killing the target in the process.
- Success chance is based on the caster and target's melee skills (melee skill XP is also used in the calculation). Max chance: 95%, Min chance: 5%
- It doesn't work on Succubi, Incubi, or Sanguophages (or any pawn with the Hemogenic gene)
- There is a small chance when draining the target that they will be transformed into a Sanguophage, instead of dying, and put into a Xenogermination coma (2 days). I plan on expanding on this in the future such as adding either a positive or negative opinion when they wake up, depending on certain conditions.
- If it fails, the target will go berserk and attack you. They will also have a -100 opinion towards you. You will also be weakened for 3 days.
- Psyfocus Cost: 50% (subject to change. I did have it set to 90% recently)
- Heat gain: 30
- Cooldown: 3 days

# Meditation Focuses
- Both Incubi, & Succubi (except w/ Draining Kiss ability), can only regain psyfocus through Sex (RJW only - see note below), or by meditating on their throne. (However, I thought this would prevent them from meditating anywhere else but they still sometimes meditated in their rooms for example. In 1.5 at least. I think this is vanilla behavior or a bug.)
- Psyfocus currently isn't gained when using the vanilla Lovin' feature. I plan on adding support for this in the future.

# Incompatibilities
- Humanoid Alien Races: It's a minor incompatibility I recently noticed. When polymorphing into human form, the red skin color isn't removed. I'm not sure why this happens as I was already removing the gene that gives this skin color from the xenotype, because it caused an issue with the wings not changing if I didn't do that. However, if you're using Biotech you probably aren't using this mod. I happened to notice it because I was recently testing out a mod that used HAR and it happened to be in my load order at the time.

# RJW Compatibility
- The Sex meditation focus from this mod is used, so that psyfocus can be regained through sex.
- The Sex need will decrease 100% faster for Incubi & Succubi. It probably sounds faster than it actually is. I recommend checking your RJW settings to make sure the Sex need decay rate is set to 100%. 

# OTYOTY / Zero's Sized Apparel Compatibility
- I modified the position of the wings and tail specifically for this mod.

# License
- [CC BY-NC-ND 4.0](creativecommons.org/licenses/by-nc-nd/4.0/deed.en)

# Planned Features
In no particular order:
- I may look into reducing the mod requirements in the future, or making them optional.
- Custom scenario(s)
- Balance changes, particularly from your feedback.
- The name & description of the xenotypes may change in the future. The names are just too good!
- Add lore for the xenotypes. I'm not sure whether I'll keep them as DND/Pathfinder inspired xenotypes, or try to integrate them further into the RimWorld lore. Or I could even release them separately.
- Prevent Succubi & Incubi from being implanted by Sanguophages (and other xenotypes, subject to review)
- Add a way for pawns to be transformed into a Succubus/Incubus e.g. through a ritual. I didn't want to use an implanter gene/ability for this.
- Ensuring offspring of the xenotypes is of the correct xenotype. I decided on making them xenotypes with xenogenes so that when polymorphing they will use their germline genes for skin and eye color. There are mods that can improve the offspring gene inheritance such as Better Gene Inheritance, but there isn't a mod to my knowledge which ensures the offspring is of the same xenotype to my knowledge (or allow you to choose). However, I plan on making the offspring xenotype selection a bit more complex depending on the parents, especially when maybe adding future demon types.
- Possibly disallowing the human form from being used indefinitely. Though, currently they are slightly hungrier in human form so that may suffice.
- Possibly having the human form be auto-equipped by the AI to remove the negative social impact and so on.
- Possibly allowing the user to choose if the negative social impact when in demon form is always active or only if they have line of sight (current default).
- Possibly changing the fact that pawns go berserk when you fail to use the draining kiss ability on them, as this does cause them to attack anyone and not just you.
- More abilities for the Succubi and Incubi?
- Both xenotypes are given traits related to their xenotype. This was so I could allow/disallow certain meditation focuses. I will see if I can change this without using traits in the future.
- Replace "Very unhappy" gene with a custom gene to something like "Chaotic" or "Unruly" instead
- Expand on targets that get turned into Sanguophages through the Draining Kiss ability, such as a positive or negative opinion towards the caster once they wake up, depending on various conditions.
- New graphics for custom genes, icons, etc.
- Animations for wings and tail would be cool (particularly for the wings when moving to show they're flying)
- Related to the above - I haven't decided if I will do this yet, but I've thought about being able to toggle flight mode and/or have it turned on/off automatically by the AI. (and maybe have it use up a resource like Stamina or simply use Sleep so they're not constantly flying)
- New demon types, such as DND/Pathfinder inspired Alu-fiends (Succubus/Human offspring), Cambions (Incubus/Human offspring), and Tieflings (offspring whose distant ancestors were demons)
- More sounds (particularly for Incubi as they don't have any at the moment)
- Also a way to toggle sounds on/off
- As you can see I have lots of ideas and things I'd like to do. I better stop here for now xD
- I'm sure I forgot something
- Have a suggestion? Let me know!

# Credits
- [Harmony](https://steamcommunity.com/sharedfiles/filedetails/?id=2009463077) (I don't currently use this at the moment but no doubt I will be in the future. However, VEF below uses this.)
- [Vanilla Expanded Framework](https://steamcommunity.com/sharedfiles/filedetails/?id=2023507013) (ignoring terrain costs for wings, forcing female or male for xenotypes, and claw hediffs/damage)
- [Alpha Genes](https://steamcommunity.com/sharedfiles/filedetails/?id=2891845502) for the claws gene icon!
- [Vanilla Races Expanded - Archon](https://steamcommunity.com/sharedfiles/filedetails/?id=3067715093) (extremely aggressive, natural psylink, & maniac genes)
- [ReSplice: Charmweavers](https://steamcommunity.com/sharedfiles/filedetails/?id=3131996691) for the lovely horns (not all horns are from this mod but most are), wings, and tail gene graphics.
- [More Mini-Horns](https://steamcommunity.com/sharedfiles/filedetails/?id=3151668881) for additional mini-horn textures for Succubi & Incubi. The default mini horn texture ("MiniHorns_east,south,north" etc) is actually higher resolution than the others and I'm trying to remember where I got it from...
- [github.com/Rimworld-Mods/Template](github.com/Rimworld-Mods/Template) for the Visual Studio Code template
- [deepai.org](deepai.org) for some images which I then modified. I'm a novice designer. I would like to have custom graphics made for everything in the future.
- Thank you to the following creators for their amazing sounds!
  - [pixabay.com/sound-effects/female-voice-burst-43245](pixabay.com/sound-effects/female-voice-burst-43245) (Succubus Death Sound)
  - [pixabay.com/sound-effects/female-compasive-voice-98423](pixabay.com/sound-effects/female-compasive-voice-98423) (Succubus Idle Sound)
  - [pixabay.com/sound-effects/female-hurt-2-94301](pixabay.com/sound-effects/female-hurt-2-94301) (Succubus Hurt Sound)
  - [pixabay.com/sound-effects/female-scream-short-251067](pixabay.com/sound-effects/female-scream-short-251067) (Succubus Hurt Sound)
  - [pixabay.com/sound-effects/giggle-339074](pixabay.com/sound-effects/giggle-339074) (Succubus Idle Sound)
  - [pixabay.com/sound-effects/giggle-86362](pixabay.com/sound-effects/giggle-86362) (Succubus Idle Sound)
  - [pixabay.com/sound-effects/girl-chucklewav-14669](pixabay.com/sound-effects/girl-chucklewav-14669) (Succubus Idle Sound)
  - [pixabay.com/sound-effects/kiss-sound-effect-43309](pixabay.com/sound-effects/kiss-sound-effect-43309) (Draining Kiss ability sound)
  - [pixabay.com/sound-effects/swoosh-6428](pixabay.com/sound-effects/swoosh-6428) (Polymorph ability sound)
- DND & Pathfinder for xenotype descriptions, ideas for the xenotype, and & ability ideas.
