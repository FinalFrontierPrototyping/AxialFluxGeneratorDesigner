import sys

sys.path.append("C:\Program Files\FreeCAD\\bin")
import os
import math
import FreeCAD
import Sketcher
import Part
import Draft

def translate(context, text):
    return text

class AFPM:
    def __init__(self, magnet_length, magnet_height, coil_count, leg_width, between_coil, wire_diameter, coil_thickness,
                 outer_radius, inner_radius):
        self.coil = FreeCAD.newDocument("Coil")
        FreeCAD.activeDocument = self.coil
        self.sketch = self.coil.addObject('Sketcher::SketchObject', 'Sketch')
        self.coil.Sketch.Placement = FreeCAD.Placement(FreeCAD.Vector(0.000000, 0.000000, 0.000000),
                                                       FreeCAD.Rotation(0.000000, 0.000000, 0.000000, 1.000000))
        self.outer_radius = float(outer_radius)
        self.inner_radius = float(inner_radius)
        self.coil_count = int(coil_count)
        self.wire_diameter = float(wire_diameter)
        self.coil_thickness = float(coil_thickness)
        self.leg_width = float(leg_width)

        self.angle_degrees = 360 / self.coil_count
        self.angle_rad = (self.angle_degrees * math.pi) / 180
        self.bottom_max_width = self.leg_width * 2
        self.rotor_radius_min = ((self.bottom_max_width + int(between_coil)) * self.coil_count) / (2 * math.pi)
        self.rotor_radius_max = self.rotor_radius_min + int(magnet_height) + self.leg_width + self.leg_width
        self.top_max_width = ((self.rotor_radius_max * 2 * math.pi) / self.coil_count) - int(between_coil)
        self.bottom_min_width = math.sqrt((pow(self.leg_width, 2) + pow(self.leg_width, 2)) - 2 * self.leg_width * self.leg_width * math.cos(self.angle_rad))
        inner_coil_radius = int(magnet_height) + self.leg_width
        self.top_min_width = math.sqrt((pow(inner_coil_radius, 2) + pow(inner_coil_radius, 2)) - (2 * inner_coil_radius * inner_coil_radius) * math.cos(self.angle_rad))
        self.coil_height = self.leg_width + int(magnet_height) + self.leg_width

        self.magnet_height = int(magnet_height)
        self.center = self.rotor_radius_min

        # outer coil coordinates
        print "Outer coil coordinates:"
        self.x0 = -(self.top_max_width / 2)
        self.y0 = self.rotor_radius_max
        print self.x0
        print self.y0

        self.x1 = -self.x0
        self.y1 = self.y0
        print self.x1
        print self.y1

        self.x2 = self.bottom_max_width / 2
        self.y2 = self.rotor_radius_min
        print self.x2
        print self.y2

        self.x3 = -self.x2
        self.y3 = self.y2
        print self.x3
        print self.y3
        print "-----------------------"

        # Top line
        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x0, self.y0, 0), FreeCAD.Vector(self.x1, self.y1, 0)))

        # Right line
        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x1, self.y1, 0), FreeCAD.Vector(self.x2, self.y2, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 0, 2, 1, 1))

        # Bottom line
        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x2, self.y2, 0), FreeCAD.Vector(self.x3, self.y3, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 1, 2, 2, 1))

        # Left line
        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x3, self.y3, 0), FreeCAD.Vector(self.x0, self.y0, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 2, 2, 3, 1))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 3, 2, 0, 1))

        print "Inner coil coordinates:"
        self.x0 = -(self.top_min_width / 2)
        self.y0 = self.rotor_radius_min + self.leg_width + self.magnet_height
        print self.x0
        print self.y0

        self.x1 = -self.x0
        self.y1 = self.y0
        print self.x1
        print self.y1

        self.x2 = self.bottom_min_width / 2
        self.y2 = self.rotor_radius_min + self.leg_width
        print self.x2
        print self.y2

        self.x3 = -self.x2
        self.y3 = self.y2
        print self.x3
        print self.y3
        print "-------------------"

        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x0, self.y0, 0), FreeCAD.Vector(self.x1, self.y1, 0)))

        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x1, self.y1, 0), FreeCAD.Vector(self.x2, self.y2, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 4, 2, 5, 1))

        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x2, self.y2, 0), FreeCAD.Vector(self.x3, self.y3, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 5, 2, 6, 1))

        self.coil.Sketch.addGeometry(Part.Line(FreeCAD.Vector(self.x3, self.y3, 0), FreeCAD.Vector(self.x0, self.y0, 0)))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 6, 2, 7, 1))
        self.coil.Sketch.addConstraint(Sketcher.Constraint('Coincident', 7, 2, 4, 1))

        self.coil.addObject("PartDesign::Pad", "Pad")
        self.coil.Pad.Sketch = self.coil.Sketch

        self.coil.Pad.Length = self.coil_thickness
        self.coil.Pad.Reversed = 0
        self.coil.Pad.Midplane = 0
        self.coil.Pad.Length2 = 100.000000
        self.coil.Pad.Type = 0
        self.coil.Pad.UpToFace = None

        self.coil.addObject("PartDesign::Fillet", "Fillet")
        self.coil.Fillet.Base = (self.coil.Pad, ["Edge1", "Edge8", "Edge2", "Edge5"])
        self.coil.Fillet.Radius = self.outer_radius

        self.coil.addObject("PartDesign::Fillet", "Fillet001")
        self.coil.Fillet001.Base = (self.coil.Fillet, ["Edge34", "Edge33", "Edge36", "Edge35"])
        self.coil.Fillet001.Radius = self.inner_radius

        self.coil.addObject("PartDesign::Fillet", "Fillet002")
        self.coil.Fillet002.Base = (self.coil.Fillet001,
                                    ["Edge32", "Edge31", "Edge27", "Edge23", "Edge19", "Edge21", "Edge25",
                                     "Edge29", "Edge8", "Edge6", "Edge4", "Edge2", "Edge1", "Edge3", "Edge5",
                                     "Edge7", "Edge12", "Edge10", "Edge9", "Edge11", "Edge13", "Edge15",
                                     "Edge16", "Edge14", "Edge37", "Edge35", "Edge39", "Edge43", "Edge47",
                                     "Edge48", "Edge45", "Edge41"])
        self.coil.Fillet002.Radius = self.wire_diameter

        Draft.makeArray(self.coil.Fillet002, FreeCAD.Vector(1, 0, 0), FreeCAD.Vector(0, 1, 0), 2, 2)
        self.coil.getObject("Array").ArrayType = "polar"
        self.coil.getObject("Array").NumberPolar = self.coil_count
        self.coil.recompute()

        path = os.getcwd()

        self.coil.saveAs(path + "\\AFPM.FCStd")

        __objs__=[]
        __objs__.append(self.coil.getObject("Array"))

        import importOBJ
        importOBJ.export(__objs__,path + u"\\AFPM.obj")

        del __objs__


if __name__ == "__main__":
    for eachArg in sys.argv:
        print eachArg

    print len(sys.argv)

    if len(sys.argv) == 1:
        afpm = AFPM(46,30 ,15, 30 ,5 ,1 ,12, 9, 4)
    else:
        afpm = AFPM(sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4], sys.argv[5], sys.argv[6], sys.argv[7],
                    sys.argv[8], sys.argv[9])

    del afpm
