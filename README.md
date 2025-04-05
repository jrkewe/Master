VR Skin Suturing Simulation (Unity)

This project is a study of the possibilities of using the Unity game engine to simulate wound suturing in Virtual Reality.

Overview

The goal of this project is to create an immersive and interactive VR application that replicates the process of suturing wounds on a realistic, deformable virtual skin model. The simulation is intended to be used for educational or training purposes.

Features

✅ Real-time skin deformation using Shader Graph

✅ Needle interaction via VR controllers

✅ Detection of puncture points for stitch tracking

✅ Modular and testable architecture (C#)

✅ Unit tests (Edit Mode) for core components

✅ Git-based workflow with modular components

Technologies

Unity (URP)

C# (Unity Engine)

Shader Graph

VR (OpenXR, Meta Quest 2/3 compatible)

Git + GitHub + LFS

NUnit (Unity Test Framework)

Architecture

SkinDeformer: Handles real-time vertex deformation and shader parameter updates

InjectionPointMarker: Detects needle collisions with injection points

NeedleTracker: Provides normalized needle direction and position

SkinColliders: Handles logic for determining entry/exit states during puncture

Goals

Develop a realistic model of elastic skin surface

Implement needle-based wound penetration and extraction

Simulate directional skin response to force

(Planned) Support stitching logic and thread simulation

Future Work

Add stitching mechanics (thread simulation)

Track progress of individual stitches

Author: Ewelina Jurkiewicz
Contact: jrkewelina@gmail.com

© 2025 Ewelina Jurkiewicz. All rights reserved.
This project is not licensed for use, distribution or modification without explicit permission.

