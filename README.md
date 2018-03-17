# GalacticMotions

> This project is based on the paper arXiv:1211.4045. The main idea is to visualize the dynamical behavior of astrophysical objects 
(e.g. galaxies) taking into account both the gravitational attraction between them and the cosmological expansion of the Universe

## Theory

We consider astrophysical objects deep inside the cell of uniformity (i.e. $R<150$Mpc). The comoving distances in the cell of uniformity are much less than 1: $li ≪ 1$ -> we can use the Cartesian coordinates. Thus, we can write

(formula)

## Modelling/Simulation

Lets take $N>>1$ (e.g. 10 000 or more) particles and model their movements in the "box": $0<x,y,z<1$, (leaving the considered region of space on the one hand, the particle gets into it with the opposite one, as in standard molecular dynamics). The distance between the particles is appropriately carried out

(formula)

The numerators of the fractions in the right-hand sides of equations (5), (6) and (7) must also have the form (formula) or (formula). In addition, in the denominators of fractions it is necessary to add a small additive number, removing possible divergences for an unrestricted approach of particles, but not significantly affecting because of their smallness.
If we take the modern average value of a typical pecular velocity equal to 300 km/s, then it will be $0.1\%$ of the speed of light $c$. Given the inverse relationship between this relationship and the large-scale factor a, we can expect that the developed mechanical approach is valid until the moment when this ratio is, for example, $1\%$. In accordance with (4), this will happen when (formula). Exactly this moment of the past can be taken as the original one.
The initial (at the found moment of the past) distribution of particles with respect to the spatial coordinates, velocities and masses is given "by hands". For example, the x-component is "physical" pecular velocity vph x = adx = dt = H0a0 ?? adx = d ?? t = H0a0l ?? ad? x = d ?? t, whence d € x = d ?? t = vphx = (H0a0l ?? a). Initial velocities d should be taken about this value, where ?? a = 0: 1, and as vph x it is necessary to take value (formula).

## Examples

